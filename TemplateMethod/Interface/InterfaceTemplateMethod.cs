using System;

namespace TemplateMethod.Interface
{
    // interfaceは、private / protected / protected internal / private protectedに出来ない
    // public または、internal は可能
    interface ITemplateMethodInterface
    {
        string Name { get; }

        string DefaultName { get; set; }

        void Method_C()
        { 
            // interface内で記述しても、このコードは実行されない
            Console.WriteLine("ITemplateMethodInterfaceMethod_C DefaultName：" + this.DefaultName);

            Console.WriteLine("ITemplateMethodInterfaceMethod_C Name：" + this.Name);
        }

        void Method_D();

    }
}