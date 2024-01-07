using System;

namespace RefactoringGuru.DesignPatterns.State.Conceptual
{
    class Context
    {
        private State _state = null;

        // Contextインスタンス生成時に、引数で状態を受け取り、状態変更メソッドを呼ぶ
        public Context(State state)
        {
            this.TransitionTo(state);
        }

        // 状態具象クラス（A or B）を受け取り、状態を変更する
        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;

            // 状態基底クラスにも、現在の状態を保持させて、Handle1/Handle2の処理を切り替える
            this._state.SetContext(this);
        }

        // 状態切り替え後に呼ばれ、状態に合った処理を行う
        public void Request1()
        {
            this._state.Handle1();
        }

        // 状態切り替え後に呼ばれ、状態に合った処理を行う
        public void Request2()
        {
            this._state.Handle2();
        }
    }

    // 状態基底クラス
    // 状態具象クラスへ継承される
    abstract class State
    {
        protected Context _context;

        public void SetContext(Context context)
        {
            this._context = context;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }

    // 状態具象クラスA
    class ConcreteStateA : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA handles request1.");
            Console.WriteLine("ConcreteStateA wants to change the state of the context.");

            // 状態がAの場合、状態Bへ切り替える
            this._context.TransitionTo(new ConcreteStateB());
        }

        // 呼ばれていない
        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA handles request2.");
        }
    }

    // 状態具象クラスB
    class ConcreteStateB : State
    {
        // 呼ばれていない
        public override void Handle1()
        {
            Console.Write("ConcreteStateB handles request1.");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB handles request2.");
            Console.WriteLine("ConcreteStateB wants to change the state of the context.");

            // 状態がBの場合、状態Aへ切り替える
            this._context.TransitionTo(new ConcreteStateA());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 状態をAにセットする
            var context = new Context(new ConcreteStateA());

            // Bへ状態を切り替える
            context.Request1();

            // Aへ状態を切り替える
            context.Request2();
        }
    }
}