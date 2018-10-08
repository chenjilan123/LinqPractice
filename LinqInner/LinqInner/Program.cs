using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqInner
{
    class Program
    {
        static void Main(string[] args)
        {
            GroupBy();

            Console.ReadKey();
        }

        private static void InnerJoin()
        {
            var numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            var labels = new string[] { "0", "1", "2", "3", "4", "5", };
            var linked1 = from number in numbers
                         join label in labels on number.ToString() equals label
                         select new { number, label };
            var linked2 = numbers
                .Join(labels, number => number.ToString(), label => label
                , (number, label) => new { number, label });
            foreach (var item in linked1)
            {
                Console.WriteLine(item);
            }
            foreach (var item in linked2)
            {
                Console.WriteLine(item);
            }

        }
        
        private static void GroupJoin()
        {
            IEnumerable<Task> tasks = ProjectData.Tasks;
            IEnumerable<Project> projects = ProjectData.Projects;
            
            var join1 = from project in projects
                        join task in tasks on project equals task.Parent into proTasks
                        select new { Project = project, TaskList = proTasks };
            var join2 = projects.GroupJoin(tasks, project => project, task => task.Parent,
                (project, taskList) => new { Project = project, TaskList = tasks });
            foreach (var project in join1)
            {
                Console.WriteLine(project.Project.Name);
                foreach (var task in project.TaskList)
                {
                    Console.WriteLine(task.Chapter);
                    Console.WriteLine(task.Name);
                }
            }
        }

        private static void GroupBy()
        {
            //创意例子：将行业统计数据根据年/月/周/日/小时逐层分组，然后可以遍历整个树。
            //嵌套语法。
            var tasks = ProjectData.Tasks as IEnumerable<Task>;

            var groupby1 = from t in tasks
                           group t by t.Parent into p
#if !GroupByNest
                           select new { Project = p.Key, Count = p.Count() };
#else
                           select new { Project = p.Key,
                               OrderedTasks = (
                                       from t in p
                                       orderby t.Name
                                       select t)
                                       .ToArray()};
#endif

            var groupby2 = tasks.GroupBy(p => p.Parent)
#if !GroupByNest
                .Select(p => new { Project = p.Key, Count = p.Count() });
#else
                .Select(p => new { Project = p.Key, 
                          OrderedTasks = (
                                       from t in p
                                       orderby t.Name
                                       select t)
                                       .ToArray()});
#endif
            foreach (var item in groupby1)
            {
                Console.WriteLine(item.Project.Name);
#if !GroupByNest
                Console.WriteLine($"Task Numbers: {item.Count}");
#else
                Console.WriteLine($"Task Numbers: {item.OrderedTasks.Length}");
#endif
            }
        }
    }
}
