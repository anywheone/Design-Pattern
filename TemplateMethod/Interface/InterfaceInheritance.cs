using System;

namespace TemplateMethod.Interface
{
    internal class InterfaceInheritance : ITemplateMethodInterface
    {
        private string _defaultName = String.Empty;

        public InterfaceInheritance() { }

        string ITemplateMethodInterface.Name => "InterfaceInheritanceName12345";

        string ITemplateMethodInterface.DefaultName { get => _defaultName; set => _defaultName = value; }

        void ITemplateMethodInterface.Method_C()
        {
            Console.WriteLine("InterfaceInheritanceMethod_C this._defaultName：" + this._defaultName);

            // 以下の方法では、ITemplateMethodInterface.Nameなどは静的なプロパティへのアクセスの為、アクセス不可。
            // インスタンス化してのアクセスとなる
            //Console.WriteLine("InterfaceInheritanceMethod_C ITemplateMethodInterface.Name：" + ITemplateMethodInterface.Name);
            //Console.WriteLine("InterfaceInheritanceMethod_C ITemplateMethodInterface.DefaultName：" + ITemplateMethodInterface.DefaultName);
        }

        void ITemplateMethodInterface.Method_D()
        {
            Console.WriteLine("InterfaceInheritanceMethod_D this._defaultName：" + this._defaultName);
        }
    }
}