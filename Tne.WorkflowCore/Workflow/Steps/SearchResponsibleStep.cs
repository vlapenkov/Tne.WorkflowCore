using Sample.TransactionalOutbox.Persistence;
using System;
using System.Threading.Tasks;
using Tne.WorkflowCore.Services;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tne.WorkflowCore
{
    public class SearchResponsibleStep : StepBodyAsync
    {

        private IDependency _dependency;
        private ShopDbContext _shopDbContext;

        static int number = 5;

        
        public SearchResponsibleStep(IDependency dependency, ShopDbContext shopDbContext)
        {
            _dependency = dependency;
            _shopDbContext = shopDbContext;
        }

        public string Responsible { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            //context.ExecutionPointer.Status = PointerStatus.Cancelled;
            
            number++;
            Console.WriteLine($"{number} started");

            //_shopDbContext.SomeEntities.Add(new DbContext.SomeEntity { Id = number, Name = $"First {number}" });
            
            //await _shopDbContext.SaveChangesAsync ();



            //Console.WriteLine("number of records is:" +_shopDbContext.SomeEntities.Count());
            
            string result = "ivanov@tne.transneft.ru";
            await _dependency.CopyData(number);
            
            

            //throw new Exception($"Error with retry {retry}");
            //Responsible = result;
            //Console.WriteLine($"Found responsible {Responsible}");
            Console.WriteLine($"{number} ended");
            return ExecutionResult.Next();

        }
    }
}