using Monadix.Kind;
using Monadix.TypeClasses.Functional;

namespace Monadix.Types.Either
{
    public abstract record Either<L, R> : Kind<Either<L>, R>;
    public record Left<L, R>(L Value) : Either<L, R>;
    public record Right<L, R>(R Value) : Either<L, R>;

    public class Either<L>
        : Monad<Either<L>>
    {
        public static Kind<Either<L>, B> Fmap<A, B>(Func<A, B> f, Kind<Either<L>, A> ex)
        {
            return ex switch
            {
                Right<L, A>(var r) => new Right<L, B>(f(r)),
                Left<L, A>(var l) => new Left<L, B>(l),
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };
        }

        public static Kind<Either<L>, A> Pure<A>(A value)
            => new Right<L, A>(value);

        public static Kind<Either<L>, B> Apply<A, B>(Kind<Either<L>, Func<A, B>> mf, Kind<Either<L>, A> mx)
            => mf switch
            {
                Left<L, Func<A, B>>(var l) => new Left<L, B>(l),
                Right<L, Func<A, B>>(var f) => mx switch
                {
                    Left<L, A>(var l) => new Left<L, B>(l),
                    Right<L, A>(var x) => new Right<L, B>(f(x)),
                    _ => throw new NotSupportedException("C# does not support discriminated union types."),
                },
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };

        public static Kind<Either<L>, B> Bind<A, B>(Kind<Either<L>, A> mx, Func<A, Kind<Either<L>, B>> f)
            => mx switch
            {
                Left<L, A>(var l) => new Left<L, B>(l),
                Right<L, A>(var r) => f(r),
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };

        public static Kind<Either<L>, A> OrElse<A>(Kind<Either<L>, A> mx, Func<A, bool> pred, Kind<Either<L>, A> my)
            => mx switch
            {
                Left<L, A> lx => lx,
                Right<L, A> rx => pred(rx.Value)
                    ? rx
                    : my,
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };
    }
}
