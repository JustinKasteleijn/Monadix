using Monadix.Kind;

namespace Monadix.Types.Either
{
    public static class EitherExtensions
    {
        public static Either<L, R> As<L, R>(this Kind<Either<L>, R> ma) =>
            (Either<L, R>)ma;
    }
}
