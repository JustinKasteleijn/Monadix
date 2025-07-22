
using Monadix.Kind;
using Monadix.TypeClasses.Functional;
using Monadix.TypeClasses.Functional.Foldable;

namespace Monadix.Types.BinaryTree
{
    public abstract record BinaryTree<A> 
        : Kind<BinaryTree, A>;

    public record Leaf<A>(A Value) : BinaryTree<A>;
    public record Empty<A>() : BinaryTree<A>;

    public record Node<A>(BinaryTree<A> Left, A Value, BinaryTree<A> Right) 
        : BinaryTree<A>;


    public class BinaryTree
        : Functor<BinaryTree>,
        Foldable<BinaryTree>
    {
        public static Kind<BinaryTree, B> Fmap<A, B>(Func<A, B> f, Kind<BinaryTree, A> mx)
        {
            return mx switch
            {
                Leaf<A>(var value) => new Leaf<B>(f(value)),
                Node<A>(var left, var val, var right) =>
                    new Node<B>(
                        Fmap(f, left).As(),
                        f(val),
                        Fmap(f, right).As()
                    ),
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };
        }

        public static S Foldl<A, S>(Kind<BinaryTree, A> ta, S initial, Func<S, A, S> f)
        {
            switch (ta)
            {
                case Leaf<A> leaf:
                    return f(initial, leaf.Value);

                case Node<A> node:
                    var leftAcc = Foldl(node.Left, initial, f);
                    var withNode = f(leftAcc, node.Value);
                    return Foldl(node.Right, withNode, f);

                default:
                    throw new NotSupportedException("C# does not support discriminated union types.");
            }
        }

        public static S Foldr<A, S>(Kind<BinaryTree, A> ta, S initial, Func<S, A, S> f)
        {
            switch (ta)
            {
                case Leaf<A> leaf:
                    return f(initial, leaf.Value);

                case Node<A> node:
                    var rightAcc = Foldr(node.Right, initial, f);
                    var withNode = f(rightAcc, node.Value);
                    return Foldr(node.Left, withNode, f);

                default:
                    throw new NotSupportedException("C# does not support discriminated union types.");
            }
        }

        public static bool IsEmpty<A>(Kind<BinaryTree, A> ta)
        {
            return ta switch
            {
                BinaryTree<A> tree => tree switch
                {
                    Empty<A> => true,
                    Leaf<A> => false,
                    Node<A> => false,
                    _ => throw new NotSupportedException("C# does not support discriminated union types."),
                },
                _ => throw new NotSupportedException("C# does not support discriminated union types."),
            };
        }
    }
}
