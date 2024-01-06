using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethod.Abstract
{
    // 派生クラス①
    // 派生クラスは抽象クラスを継承して、必要な数だけ作成する
    // 抽象クラスでabstractキーワードが記述されたプロパティやメソッドは、実装必須となる
    // クラスを右クリックして、クイックアクションとリファクタリングから、
    // abstractとなっているもののテンプレートを簡単に記述出来る
    internal class AbstractInheritance : AbstractTemplateMethod
    {
        // 派生クラスで実装ない為、抽象クラスのコンストラクタのみコールされる
        // public AbstractInheritance() { }

        // 派生クラスで実装がある為、
        // 抽象クラスのコンストラクタがコールされ、次に派生クラスコンストラクタがコールされる
        public AbstractInheritance()
        {
            // Constructor
            Console.WriteLine("AbstractInheritance \"Constructor\"");

            // DefaultNameを上書き
            this.DefaultName = "AbstractInheritanceDefaultName";
        }

        // protected override string Name => "AbstractInheritance";

        // Nameが空のケース 読み取り専用プロパティだが、overrideする事でsetも可能となる
        protected override string Name => "";

        public override void Method_B()
        {
            Console.WriteLine("Method_B Name：" + this.Name);
        }
    }
}