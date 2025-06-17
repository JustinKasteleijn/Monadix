using Monadix.Kind;

namespace Monadix.Types.Maybe
{
    public static class MaybeExtensions
    {
        public static Maybe<A> As<A>(this Kind<Maybe, A> mx)
            => (Maybe<A>)mx;
    }
}
