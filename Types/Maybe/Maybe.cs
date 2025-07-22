using Monadix.Kind;
using Monadix.TypeClasses.Functional;
using Monadix.TypeClasses.Functional.Foldable;

namespace Monadix.Types.Maybe
{
    public abstract record Maybe<A> : Kind<Maybe, A>;
    public record Just<A>(A Value) : Maybe<A>;
    public record Nothing<A>() : Maybe<A>;

    public class Maybe
        : Monad<Maybe>,
        Foldable<Maybe>
    {
        public static Kind<Maybe, B> Fmap<A, B>(Func<A, B> f, Kind<Maybe, A> mx)
            => mx switch
            {
                Just<A>(var x) => new Just<B>(f(x)),
                Nothing<A> => new Nothing<B>(),
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };

        public static Kind<Maybe, A> Pure<A>(A x)
            => new Just<A>(x);

        public static Kind<Maybe, B> Apply<A, B>(Kind<Maybe, Func<A, B>> mf, Kind<Maybe, A> mx)
            => mf switch
            {
                Just<Func<A, B>>(var f) => mx switch
                {
                    Just<A>(var x) => new Just<B>(f(x)),
                    Nothing<A> => new Nothing<B>(),
                    _ => throw new NotSupportedException("C# does not support discriminated union types."),
                },
                Nothing<Func<A, B>> => new Nothing<B>(),
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };

        public static Kind<Maybe, B> Bind<A, B>(Kind<Maybe, A> mx, Func<A, Kind<Maybe, B>> f)
            => mx switch
                {
                    Just<A>(var x) => f(x),
                    Nothing<A> => new Nothing<B>(),
                    _ => throw new NotSupportedException("C# does not support discriminated union types."),
                };

        public static S Foldr<A, S>(Kind<Maybe, A> ma, S initial, Func<S, A, S> f)
            => ma switch
            {
                Just<A>(var x) => f(initial, x),
                Nothing<A> => initial,
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };

        public static S Foldl<A, S>(Kind<Maybe, A> ma, S initial, Func<S, A, S> f)
            => Foldr(ma, initial, f);

        public static Kind<Maybe, A> Try<A>(Func<A> f)
        {
            try
            {
                return new Just<A>(f());
            } catch (Exception)
            {
                return new Nothing<A>();
            }
        }
    }
}
