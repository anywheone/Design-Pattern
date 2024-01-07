using System;

namespace RefactoringGuru.DesignPatterns.Command.Conceptual
{
    // 実行コマンドのインターフェース
    public interface ICommand
    {
        void Execute();
    }

    // シンプルな実行コマンドを実装
    class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;

        // コンストラクタで文字列を受け取る
        public SimpleCommand(string payload)
        {
            this._payload = payload;
        }

        // SimpleCommandでは、コンソール出力のみ
        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things like printing ({this._payload})");
        }
    }

    // 複雑な実行コマンドを実装
    class ComplexCommand : ICommand
    {
        private Receiver _receiver;

        private string _a;

        private string _b;

        // コンストラクタで、Receiver（DoSomethingとDoSomethingElseメソッドを持つ）と文字列a, bを受け取る
        public ComplexCommand(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }

        // ComplexCommandでは、コンソール出力と、受信者側の処理を２つ実行する
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");

            // コンストラクタで受け取ったReceiverのメソッドを実行する
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }

    // ComplexCommandクラスのコマンド実行に使用される処理群
    class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }

    // Mainプロセスでインスタンス化され、コマンドの準備・呼び出し者として使用されるクラス
    class Invoker
    {
        private ICommand _onStart;

        private ICommand _onFinish;

        // コマンド具象クラスを受け取る（ICommandを継承しているSimpleCommand or ComplexCommand）
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }

        // コマンド具象クラスを受け取る（ICommandを継承しているSimpleCommand or ComplexCommand）
        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }

        // 上記Set処理後に実行する、メイン処理
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I begin?");
            if (this._onStart is ICommand)
            {
                // 事前にセットされたSimpleCommand or ComplexCommandのコマンドを実行する
                this._onStart.Execute();
            }

            Console.WriteLine("Invoker: ...doing something really important...");

            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            if (this._onFinish is ICommand)
            {
                // 事前にセットされたSimpleCommand or ComplexCommandのコマンドを実行する
                this._onFinish.Execute();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // コマンド呼び出し者
            Invoker invoker = new Invoker();

            // SimpleCommand（具象コマンド）をセットする
            invoker.SetOnStart(new SimpleCommand("Say Hi!"));

            // 複雑なコマンドで利用されるメソッド群を持つReceiver
            Receiver receiver = new Receiver();

            // ComplexCommand（具象コマンド）をセットする
            invoker.SetOnFinish(new ComplexCommand(receiver, "Send email", "Save report"));

            // 上記Set後に、実行するメイン処理
            // セットしたコマンドにより処理は可変
            invoker.DoSomethingImportant();
        }
    }
}