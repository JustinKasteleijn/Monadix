using Monadix.Kind;

namespace Monadix.Types.BinaryTree
{
        public static class BinaryTreeExtensions
        {
            public static BinaryTree<T> As<T>(this Kind<BinaryTree, T> ma) =>
                (BinaryTree<T>)ma;
        }
}
