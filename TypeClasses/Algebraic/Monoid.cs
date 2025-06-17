namespace Monadix.TypeClasses.Algebraic
{
    public interface Monoid<A>
        : Semigroup<A>
    {
        static abstract A Empty();
    }
}
