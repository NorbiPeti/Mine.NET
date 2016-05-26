using Mine.NET.entity;

namespace Mine.NET.Event.entity
{
    /**
     * Called when a sheep regrows its wool
     */
    public class SheepRegrowWoolEventArgs : EntityEventArgs<Sheep>, Cancellable {
        private bool cancel;

        public SheepRegrowWoolEventArgs(Sheep sheep) : base(sheep)
        {
            this.cancel = false;
        }

        public bool isCancelled() {
            return cancel;
        }

        public void setCancelled(bool cancel) {
            this.cancel = cancel;
        }

    }
}
