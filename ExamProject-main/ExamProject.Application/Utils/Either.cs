namespace ExamProject.Application.Utils {

    public class Either<L, R> {
        public L Left { get; }
        public R Right { get; }

        public bool IsSuccess { get; }

        private Either(L left) {
            Left = left;
            IsSuccess = false;
        }

        private Either(R right) {
            Right = right;
            IsSuccess = true;
        }

        public static Either<L, R> Failure(L left) => new Either<L, R>(left);

        public static Either<L, R> Success(R right) => new Either<L, R>(right);
    }
}