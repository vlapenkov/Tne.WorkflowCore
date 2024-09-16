namespace Tne.WorkflowCore.Services
{
    public class Dependency : IDependency
    {
        public async Task CopyData(int multiplier)
        {

            //long calc=0;
            //for (int i = 0; i < 1_000_000_0*multiplier; i++)
            //{
            //    if (i%2==0)
            //    calc = +i;
            //}


            await Task.Delay(1_000);

            //throw new Exception("Some eror started");

            //Console.WriteLine(calc);
           
        }
    }
}
