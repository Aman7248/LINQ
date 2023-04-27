using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Emp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeptId { get; set; }
    }
    class Dept
    {
        public int Id { get; set; }
        public string DeptName { get; set; }
    }
    internal class Linq
    {
        static void Main()
        {
            var empList = new List<Emp>()
            {
                new Emp { Id = 1, Name = "Aman", DeptId = 10 },
                new Emp { Id = 2, Name = "Kiran", DeptId = 20 },
                new Emp { Id = 1, Name = "duplicateAman", DeptId = 30 },
            };

            var deptList = new List<Dept>()
            {
                new Dept { Id = 10, DeptName ="IT"},
                new Dept { Id = 20, DeptName ="HR"},
                new Dept { Id = 40, DeptName ="Admin"}
            };

            Console.WriteLine("---------------------Filters------------------------------");
            //Filters
            var filterLambdaResult = empList.Where(emp => emp.Id == 1).ToList();
            var filterQueryResult = from emp in empList
                                    where emp.DeptId == 10
                                    select emp;

            var andResult = empList.Where(emp=>emp.Id==1 || emp.DeptId==20).ToList();

            /*foreach (var obj in andResult)
            {
                Console.WriteLine(obj.Id + " " + obj.Name);
            }*/

            Console.WriteLine("---------------------------------------------------");
            //inner join
            var result = from e in empList
                           join d in deptList
                           on e.DeptId equals d.Id
                           select new { e.Id, e.Name, d.DeptName };

            var lambdaResult = empList.Join(deptList, e=>e.DeptId, d=>d.Id, 
                (e, d) => new {e.Id, e.Name, d.DeptName});

            /*foreach(var obj in lambdaResult)
            {
                Console.WriteLine(obj.Id +" "+obj.Name+" "+obj.DeptName);
            }*/

            Console.WriteLine("---------------------------------------------------");

        //left join
            var leftResult = from e in empList
                             join d in deptList
                             on e.DeptId equals d.Id
                             into empDeptGroup
                             from dept in empDeptGroup.DefaultIfEmpty()
                             select new { e, dept };

            var leftLambdaResult = empList.GroupJoin(deptList, e=>e.DeptId, d=>d.Id,
                (e, d) => new {e, d}
                ).SelectMany(
                    x=> x.d.DefaultIfEmpty(),

                    (em, de) => new {em.e, de}
                    );

            /*foreach (var obj in leftLambdaResult)
            {
                Console.WriteLine(obj.e.Id + " " + obj.e.Name + " " + obj.de?.DeptName);
            }*/
            Console.WriteLine("---------------------------------------------------");

            //right join
            var rightResult = from d in deptList
                              join e in empList
                              on d.Id equals e.DeptId
                              into empdeptGroup
                              from emp in empdeptGroup.DefaultIfEmpty()
                              select new { emp, d };

            var rightLambdaResult = deptList.GroupJoin(empList, d => d.Id, e => e.DeptId,
                (d, e) => new { d, e }
                ).SelectMany(
                    x=>x.e.DefaultIfEmpty(),

                    (de, em)=> new {de.d, em}
                );

            /*foreach (var obj in rightLambdaResult)
            {
                Console.WriteLine(obj.em?.Id + " " + obj.em?.Name + " " + obj.d.DeptName);
            }*/
            Console.WriteLine("---------------------------------------------------");
        }
    }
}
