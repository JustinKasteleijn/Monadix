namespace Monadix.TypeClasses.Algebraic
{
    public interface Monoid<A>
        : Semigroup<A>
    {
        public static abstract A Empty();
    }
}
