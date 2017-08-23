namespace LittleBlog.Entities.Shared
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj == this)
                return true;

            if (obj.GetType() != this.GetType())
                return false;

            return ((Entity)obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}