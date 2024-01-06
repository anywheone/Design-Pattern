using TemplateMethod;
using TemplateMethod.Abstract;
using TemplateMethod.Interface;

internal class Program
{
    // ☆はコメントアウト解除で処理動作の確認をする可能箇所
    // ◆はTips
    private static void Main(string[] args)
    {
        Console.WriteLine("TemplateMethod.Abstract");

        // ☆派生クラスでコンストラクタを一切記述しなくても、抽象クラスのコンストラクタがコールされる
        AbstractInheritance abstractInheritance = new AbstractInheritance();

        abstractInheritance.Method_A();
        abstractInheritance.Method_B();

        // ◆抽象クラスのインスタンス作成は出来ない
        // AbstractTemplateMethod abstractTemplateMethod = new AbstractTemplateMethod();


        Console.WriteLine("TemplateMethod.Interface");

        InterfaceInheritance interfaceInheritance = new InterfaceInheritance();

        ((ITemplateMethodInterface)interfaceInheritance).DefaultName = "www";
        Console.WriteLine(((ITemplateMethodInterface)interfaceInheritance).Name);

        ((ITemplateMethodInterface)interfaceInheritance).Method_C();

        ((ITemplateMethodInterface)interfaceInheritance).Method_D();

        Console.ReadKey();
    }
}