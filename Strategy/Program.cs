using System;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Strategy.Conceptual
{
    class Context
    {
        private IStrategy _strategy;

        public Context()
        { }

        // 使用なし（自クラスのnewと同時にストラテジーをセットできるコンストラクタ）
        //public Context(IStrategy strategy)
        //{
        //    this._strategy = strategy;
        //}

        // ストラテジーをセット
        // 具象ストラテジーA or Bのクラスインスタンスを引数で渡して、Contextクラス（自クラス）のフィールドへセットする
        // IStrategyを継承しているクラスインスタンスを引数で受け取る事ができる
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Sorting data using the strategy (not sure how it'll do it)");

            // セットされたストラテジー（this._strategy）で処理が切り替わる
            // 具象ストラテジークラス側で何を返しても良いように、型はvarで定義している
            var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;

            // stringListにキャスト（AとBの具象ストラテジークラスではList<string>型で返している）
            foreach (var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }
    }

    // Contextクラスが、いづれかの具象ストラテジークラスにアクセスする為のインターフェース
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // 具象ストラテジーA（切り替え処理A）
    class ConcreteStrategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            // object→stringListへキャスト
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    // 具象ストラテジーB（切り替え処理B）
    class ConcreteStrategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            // object→stringListへキャスト
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context();

            Console.WriteLine("Client: Strategy is set to normal sorting.");

            // ConcreteStrategyAがインターフェースを継承しており、ストラテジーインスタンスをコンテキストクラスへセット
            context.SetStrategy(new ConcreteStrategyA());

            // セットしたストラテジーに基づいたビジネスロジックを実行する
            context.DoSomeBusinessLogic();

            Console.WriteLine();

            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            context.SetStrategy(new ConcreteStrategyB());
            context.DoSomeBusinessLogic();
        }
    }
}