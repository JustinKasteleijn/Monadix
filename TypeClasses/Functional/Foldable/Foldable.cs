using Monadix.Kind;
using Monadix.TypeClasses.Algebraic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Monadix.TypeClasses.Functional.Foldable
{
    public interface Foldable<T>
        where T : Foldable<T>
    {
        public static abstract S Foldr<A, S>(Kind<T, A> ta, S initial, Func<S, A, S> f);
        public static abstract S Foldl<A, S>(Kind<T, A> ta, S initial, Func<S, A, S> f);

        public static virtual bool IsEmpty<A>(Kind<T, A> ta) =>
        T.Foldr(ta, true, (_, _) => false);

        public static virtual int Count<A>(Kind<T, A> ta) =>
            T.Foldr(ta, 0, (s, _) => s + 1);

        public static virtual A Sum<A>(Kind<T, A> ta)
            where A : INumber<A> =>
            T.Foldr(ta, A.Zero, (s, x) => s + x);

        public static virtual A Fold<A>(Kind<T, A> ta)
            where A : Monoid<A> =>
            T.Foldr(ta, A.Empty(), A.Concat);

        public static virtual IEnumerable<A> AsEnumerable<A>(Kind<T, A> ta)
        => ta.Foldr(new List<A>(), (s, x) =>
            {
                s.Add(x);
                return s;
            });

        public static virtual bool All<A>(Kind<T, A> ta, Func<A, bool> f) =>
            T.Foldr(ta, true, (s, x) => s && f(x));

        public static virtual bool Any<A>(Kind<T, A> ta, Func<A, bool> f) =>
            T.Foldr(ta, false, (s, x) => s || f(x));

        public static virtual bool Contains<A>(Kind<T, A> ta, A value)
            where A : IEquatable<A> =>
            T.Any(ta, x => x.Equals(value));
    }
}
