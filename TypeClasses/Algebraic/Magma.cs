namespace Monadix.TypeClasses.Algebraic
{
    public interface Magma<A>
    {
        static abstract A Concat (A a, A b);
    }
}
