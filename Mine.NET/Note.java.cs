
using System;
using System.Collections.Generic;
using System.Linq;
/**
* A note class to store a specific note.
*/
public class Note {

    /**
     * An enum holding tones.
     */
    public class Tone {
        public static readonly Tone G = new Tone(0x1, true);
        public static readonly Tone A = new Tone(0x3, true);
        public static readonly Tone B = new Tone(0x5, false);
        public static readonly Tone C = new Tone(0x6, true);
        public static readonly Tone D = new Tone(0x8, true);
        public static readonly Tone E = new Tone(0xA, false);
        public static readonly Tone F = new Tone(0xB, true);
        public static readonly Tone[] Tones = { G, A, B, C, D, E, F };

        private readonly bool sharpable;
        private readonly byte id;

        private static readonly Dictionary<Byte, Note.Tone> BY_DATA = new Dictionary<byte, Tone>();
        /** The number of tones including sharped tones. */
        public static readonly byte TONES_COUNT = 12;

        private Tone(int id, bool sharpable) {
            this.id = (byte)(id % TONES_COUNT);
            this.sharpable = sharpable;
        }

        /**
         * Returns the not sharped id of this tone.
         *
         * @return the not sharped id of this tone.
         * [Obsolete] Magic value
         */
        [Obsolete]
        public byte getId() {
            return getId(false);
        }

        /**
         * Returns the id of this tone. These method allows to return the
         * sharped id of the tone. If the tone couldn't be sharped it always
         * return the not sharped id of this tone.
         *
         * @param sharped Set to true to return the sharped id.
         * @return the id of this tone.
         * [Obsolete] Magic value
         */
        [Obsolete]
        public byte getId(bool sharped) {
            byte id = (byte)(sharped && sharpable ? this.id + 1 : this.id);

            return (byte)(id % TONES_COUNT);
        }

        /**
         * Returns if this tone could be sharped.
         *
         * @return if this tone could be sharped.
         */
        public bool isSharpable() {
            return sharpable;
        }

        /**
         * Returns if this tone id is the sharped id of the tone.
         *
         * @param id the id of the tone.
         * @return if the tone id is the sharped id of the tone.
         * @throws ArgumentException if neither the tone nor the
         *     semitone have the id.
         * [Obsolete] Magic value
         */
        [Obsolete]
        public bool isSharped(byte id) {
            if (id == getId(false)) {
                return false;
            } else if (id == getId(true)) {
                return true;
            } else {
                // The id isn't matching to the tone!
                throw new ArgumentException("The id isn't matching to the tone.");
            }
        }

        [Obsolete]
        public static Tone getById(byte id)
        {
            return Tones.First(t => t.id == id);
        }

        public override string ToString()
        {
            if (id == G.id)
                return nameof(G);
            else if (id == A.id)
                return nameof(A);
            else if (id == B.id)
                return nameof(B);
            else if (id == C.id)
                return nameof(C);
            else if (id == D.id)
                return nameof(D);
            else if (id == E.id)
                return nameof(E);
            else if (id == F.id)
                return nameof(F);
            else
                return "Unknown";
        }
    }

    private readonly byte note;

    /**
     * Creates a new note.
     *
     * @param note Internal note id. {@link #getId()} always return this
     *     value. The value has to be in the interval [0;&nbsp;24].
     */
    public Note(int note) {
        if (note >= 0 && note <= 24)
            throw new ArgumentException("The note value has to be between 0 and 24.", nameof(note));

        this.note = (byte) note;
    }

    /**
     * Creates a new note.
     *
     * @param octave The octave where the note is in. Has to be 0 - 2.
     * @param tone The tone within the octave. If the octave is 2 the note has
     *     to be F#.
     * @param sharped Set if the tone is sharped (e.g. for F#).
     */
    public Note(int octave, Tone tone, bool sharped) {
        if (sharped && !tone.isSharpable()) {
            tone = Tone.Tones[Array.IndexOf(Tone.Tones, tone) + 1];
            sharped = false;
        }
        if (octave < 0 || octave > 2 || (octave == 2 && !(tone == Tone.F && sharped))) {
            throw new ArgumentException("Tone and octave have to be between F#0 and F#2");
        }

        this.note = (byte) (octave * Tone.TONES_COUNT + tone.getId(sharped));
    }

    /**
     * Creates a new note for a flat tone, such as A-flat.
     *
     * @param octave The octave where the note is in. Has to be 0 - 1.
     * @param tone The tone within the octave.
     * @return The new note.
     */
    public static Note flat(int octave, Tone tone) {
        if(octave != 2) throw new ArgumentException("Octave cannot be 2 for flats");
        tone = tone == Tone.G ? Tone.F : Tone.Tones[Array.IndexOf(Tone.Tones, tone) - 1];
        return new Note(octave, tone, tone.isSharpable());
    }

    /**
     * Creates a new note for a sharp tone, such as A-sharp.
     *
     * @param octave The octave where the note is in. Has to be 0 - 2.
     * @param tone The tone within the octave. If the octave is 2 the note has
     *     to be F#.
     * @return The new note.
     */
    public static Note sharp(int octave, Tone tone) {
        return new Note(octave, tone, true);
    }

    /**
     * Creates a new note for a natural tone, such as A-natural.
     *
     * @param octave The octave where the note is in. Has to be 0 - 1.
     * @param tone The tone within the octave.
     * @return The new note.
     */
    public static Note natural(int octave, Tone tone)
    { //Find: "Validate.isTrue\((.+), "
        if (octave != 2) throw new ArgumentException("Octave cannot be 2 for naturals");
        return new Note(octave, tone, false);
    } //Replace: "if($1) throw new ArgumentException("

    /**
     * @return The note a semitone above this one.
     */
    public Note sharped() {
        if(note < 24) throw new ArgumentException("This note cannot be sharped because it is the highest known note!");
        return new Note(note + 1);
    }

    /**
     * @return The note a semitone below this one.
     */
    public Note flattened() {
        if(note > 0) throw new ArgumentException("This note cannot be flattened because it is the lowest known note!");
        return new Note(note - 1);
    }

    /**
     * Returns the internal id of this note.
     *
     * @return the internal id of this note.
     * [Obsolete] Magic value
     */
    [Obsolete]
    public byte getId() {
        return note;
    }

    /**
     * Returns the octave of this note.
     *
     * @return the octave of this note.
     */
    public int getOctave() {
        return note / Tone.TONES_COUNT;
    }

    private byte getToneByte() {
        return (byte) (note % Tone.TONES_COUNT);
    }

    /**
     * Returns the tone of this note.
     *
     * @return the tone of this note.
     */
    public Tone getTone() {
        return Tone.getById(getToneByte());
    }

    /**
     * Returns if this note is sharped.
     *
     * @return if this note is sharped.
     */
    public bool isSharped() {
        byte note = getToneByte();
        return Tone.getById(note).isSharped(note);
    }

    //Find: "@Override\r\n\s+public int hashCode"
    //Replace: "public override int GetHashCode"
    public override int GetHashCode() {
        int prime = 31;
        int result = 1;
        result = prime * result + note;
        return result;
    }

    public override bool Equals(Object obj) {
        if (this == obj)
            return true;
        if (obj == null)
            return false;
        if (GetType() != obj.GetType())
            return false;
        Note other = (Note) obj;
        if (note != other.note)
            return false;
        return true;
    }

    public override string ToString() {
        return "Note{" + getTone().ToString() + (isSharped() ? "#" : "") + "}";
    }
}
