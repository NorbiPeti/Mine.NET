package org.bukkit.configuration.file;

import java.util.LinkedHashMap;
import java.util.Map;

import org.bukkit.configuration.ConfigurationSection;
import org.bukkit.configuration.serialization.ConfigurationSerializable;
import org.bukkit.configuration.serialization.ConfigurationSerialization;

import org.yaml.snakeyaml.nodes.Node;
import org.yaml.snakeyaml.representer.Representer;

public class YamlRepresenter : Representer {

    public YamlRepresenter() {
        this.multiRepresenters.Add(ConfigurationSection.class, new RepresentConfigurationSection());
        this.multiRepresenters.Add(ConfigurationSerializable.class, new RepresentConfigurationSerializable());
    }

    private class RepresentConfigurationSection : RepresentMap {
        public override Node representData(Object data) {
            return base.representData(((ConfigurationSection) data).getValues(false));
        }
    }

    private class RepresentConfigurationSerializable : RepresentMap {
        public override Node representData(Object data) {
            ConfigurationSerializable serializable = (ConfigurationSerializable) data;
            Dictionary<String, Object> values = new LinkedHashMap<String, Object>();
            values.Add(ConfigurationSerialization.SERIALIZED_TYPE_KEY, ConfigurationSerialization.getAlias(serializable.getClass()));
            values.putAll(serializable.serialize());

            return base.representData(values);
        }
    }
}
