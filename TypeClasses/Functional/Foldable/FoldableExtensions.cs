using Monadix.TypeClasses.Algebraic;
using Monadix.Kind;
using System.Numerics;

namespace Monadix.TypeClasses.Functional.Foldable
{
    public static class FoldableExtensions
    {
        public static Kind<F, B> Select<F, A, B>(this Kind<F, A> fa, Func<A, B> f)
            where F : Functor<F> =>
            F.Fmap(f, fa);

        public static Kind<F, B> Map<F, A, B>(this Kind<F, A> fa, Func<A, B> f)
            where F : Functor<F> =>
            F.Fmap(f, fa);

        public static S Foldr<T, A, S>(this Kind<T, A> ta, S initial, Func<S, A, S> f)
            where T : Foldable<T> =>
            T.Foldr(ta, initial, f);

        public static S Foldl<T, A, S>(this Kind<T, A> ta, S initial, Func<S, A, S> f)
            where T : Foldable<T> =>
            T.Foldr(ta, initial, f);

        public static bool IsEmpty<T, A>(this Kind<T, A> ta)
            where T : Foldable<T> =>
            T.IsEmpty(ta);

        public static int Count<T, A>(this Kind<T, A> ta)
            where T : Foldable<T> =>
            T.Count(ta);

        public static A Sum<T, A>(this Kind<T, A> ta)
            where T : Foldable<T>
            where A : INumber<A> =>
            T.Sum(ta);

        public static A Fold<T, A>(this Kind<T, A> ta)
            where T : Foldable<T>
            where A : Monoid<A> =>
            T.Fold(ta);

        public static IEnumerable<A> AsEnumerable<T, A>(this Kind<T, A> ta)
            where T : Foldable<T> =>
            T.AsEnumerable(ta);

        public static bool All<T, A>(this Kind<T, A> ta, Func<A, bool> f)
            where T : Foldable<T> =>
            T.All(ta, f);

        public static bool Any<T, A>(this Kind<T, A> ta, Func<A, bool> f)
            where T : Foldable<T> =>
            T.Any(ta, f);

        public static bool Contains<T, A>(this Kind<T, A> ta, A value)
            where A : IEquatable<A>
            where T : Foldable<T> =>
            T.Contains(ta, value);
    }
}
