using FubuLocalization;

namespace FubuMVC.Instrumentation.Navigation
{
    public class InstrumentationKeys : StringToken
    {
        public static readonly InstrumentationKeys Routes = new InstrumentationKeys("Instrumentation");

        public InstrumentationKeys(string defaultValue) : base(null, defaultValue, namespaceByType: true)
        {
        }

        public bool Equals(InstrumentationKeys other)
        {
            return other.Key.Equals(Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as InstrumentationKeys);
        }

        public override int GetHashCode()
        {
            return ("InstrumentationKeys:" + Key).GetHashCode();
        }
    }
}