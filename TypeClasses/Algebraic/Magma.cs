namespace Monadix.TypeClasses.Algebraic
{
    public interface Magma<A>
    {
        public static abstract A Concat (A a, A b);
    }
}
