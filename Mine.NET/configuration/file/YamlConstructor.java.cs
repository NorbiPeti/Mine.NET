package org.bukkit.configuration.file;

import java.util.LinkedHashMap;
import java.util.Map;

import org.yaml.snakeyaml.nodes.Node;
import org.yaml.snakeyaml.constructor.SafeConstructor;
import org.yaml.snakeyaml.error.YAMLException;
import org.yaml.snakeyaml.nodes.Tag;

import org.bukkit.configuration.serialization.ConfigurationSerialization;

public class YamlConstructor : SafeConstructor {

    public YamlConstructor() {
        this.yamlConstructors.Add(Tag.MAP, new ConstructCustomObject());
    }

    private class ConstructCustomObject : ConstructYamlMap {
        public override Object construct(Node node) {
            if (node.isTwoStepsConstruction()) {
                throw new YAMLException("Unexpected referential mapping structure. Node: " + node);
            }

            Dictionary<?, ?> raw = (Dictionary<?, ?>) base.construct(node);

            if (raw.containsKey(ConfigurationSerialization.SERIALIZED_TYPE_KEY)) {
                Dictionary<String, Object> typed = new LinkedHashMap<String, Object>(raw.Count);
                foreach (KeyValuePair<?, ?> entry  in  raw.entrySet()) {
                    typed.Add(entry.Key.ToString(), entry.Value);
                }

                try {
                    return ConfigurationSerialization.deserializeObject(typed);
                } catch (ArgumentException ex) {
                    throw new YAMLException("Could not deserialize object", ex);
                }
            }

            return raw;
        }

        public override void construct2ndStep(Node node, Object object) {
            throw new YAMLException("Unexpected referential mapping structure. Node: " + node);
        }
    }
}
