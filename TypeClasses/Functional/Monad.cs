using Monadix.Kind;

namespace Monadix.TypeClasses.Functional
{
    public interface Monad<F>
        : Applicative<F>
        where F : Applicative<F>
    {
        static abstract Kind<F, B> Bind<A, B>(Kind<F, A> mx, Func<A, Kind<F, B>> f);
    }
}
