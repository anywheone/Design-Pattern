using System;

namespace TemplateMethod.Abstract
{

    // テンプレートメソッドについて・・・
    // 可変・不変のデータを割り出して、不変データを抽象クラスで定義
    // 可変データを派生クラス側で持たせる
    // メソッドも派生クラスで共通のメソッドは抽象クラス内に実装する
    // 派生クラスで実装が必要な場合は、抽象メソッドとしてテンプレートだけ定義しておく

    // 抽象クラス
    // 抽象クラスは直接インスタンスは作成できない
    // abstract classの場合、実装ありも、実装なしも、両方記述出来る
    abstract internal class AbstractTemplateMethod
    {
        // getのみだが、派生クラスで「override」する事で、setも出来る
        protected abstract string Name { get; }

        // setを付与しているが、これは抽象クラス側でsetする為
        protected string DefaultName { get; set; }

        // publicであっても抽象クラスなのでインスタンス作成は出来ない
        public AbstractTemplateMethod()
        {
            // Constructor
            Console.WriteLine("AbstractTemplateMethod \"Constructor\"");

            DefaultName = "DefaultName";

            // setなしの読み取り専用の為、Nameに抽象クラスでは値をセットできない
            // overrideした派生クラス側であれば、読み取り専用プロパティでもset可能となる
            // this.Name = "Something";
        }

        // 抽象クラスで実装され、派生クラスでも共通利用する想定のメソッド
        public void Method_A()
        {
            if (string.IsNullOrEmpty(Name))
            {
                // 抽象クラスのコンストラクタを派生クラスでコールした場合、DefaultName
                Console.WriteLine("Method_A DefaultName：" + DefaultName);
            }
            else
            {
                // 抽象クラスのコンストラクタを派生クラスでコールしていない場合、Name
                Console.WriteLine("Method_A Name：" + Name);
            }
        }

        // 実装なしの為、派生クラスで実装する想定のメソッド
        public abstract void Method_B();

    }
}