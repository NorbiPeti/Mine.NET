using System;

namespace Mine.NET.material
{
    /**
     * Represents the tripwire
     */
    public class Tripwire : MaterialData
    {

        public Tripwire() : base(Materials.TRIPWIRE)
        {
        }

        private bool activated;
        /**
         * Test if tripwire is currently activated
         *
         * @return true if activated, false if not
         */
        public bool isActivated()
        {
            //return (getData() & 0x4) != 0;
            return activated;
        }

        /**
         * Set tripwire activated state
         *
         * @param act - true if activated, false if not
         */
        public void setActivated(bool act)
        {
            /*int dat = getData() & (0x8 | 0x3);
            if (act)
            {
                dat |= 0x4;
            }
            setData((byte)dat);*/
            activated = act;
        }

        private bool objecttriggering;
        /**
         * Test if object triggering this tripwire directly
         *
         * @return true if object activating tripwire, false if not
         */
        public bool isObjectTriggering()
        {
            //return (getData() & 0x1) != 0;
            return objecttriggering;
        }

        /**
         * Set object triggering state for this tripwire
         *
         * @param trig - true if object activating tripwire, false if not
         */
        public void setObjectTriggering(bool trig)
        {
            /*int dat = getData() & 0xE;
            if (trig)
            {
                dat |= 0x1;
            }
            setData((byte)dat);*/
            objecttriggering = trig;
        }

        public new Tripwire Clone() { return (Tripwire)base.Clone(); }

        public override string ToString()
        {
            return base.ToString() + (isActivated() ? " Activated" : "") + (isObjectTriggering() ? " Triggered" : "");
        }
    }
}
