using Monadix.Kind;

namespace Monadix.TypeClasses.Functional
{
    public interface Applicative<F>
        : Functor<F> 
        where F : Functor<F>
    {
        static abstract Kind<F, A> Pure<A>(A value);
        static abstract Kind<F, B> Apply<A, B>(Kind<F, Func<A, B>> f, Kind<F, A> mx);
    }
}
