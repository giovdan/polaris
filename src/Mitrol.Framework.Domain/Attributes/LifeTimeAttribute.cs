namespace Mitrol.Framework.Domain.Attributes
{
    using System;
    public class LifeTimeAttribute: Attribute
    {
        public int LifeTime { get; set; }
        public LifeTimeAttribute(int lifeTime)
        {
            LifeTime = lifeTime;
        }
    }
}
