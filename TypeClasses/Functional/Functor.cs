using Monadix.Kind;

namespace Monadix.TypeClasses.Functional
{
    public interface Functor<F>
        where F: Functor<F>
    {
        static abstract Kind<F, B> Fmap<A, B> (Func<A, B> f, Kind<F, A> x);
    }
}
