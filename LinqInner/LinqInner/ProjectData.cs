using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LinqInner
{
    public class ProjectData
    {
        public static List<Project> Projects = new List<Project>();
        public static List<Task> Tasks = new List<Task>();
        static ProjectData()
        {
            InitProjectData();
        }
        public static void InitProjectData()
        {
            var xmlDoc = XDocument.Load("ProjectTasks.xml");
            var root = xmlDoc.Root;
            var projects = (from e in root.Elements("Project")
                        select new Project()
                        {
                            Id = (int)e.Element("Id"),
                            Name = (string)e.Element("Name"),
                            Tasks = (
                                    from e1 in e.Element("Tasks").Elements("Task")
                                    select new Task()
                                    {
                                        Chapter = (int)e1.Element("Chapter"),
                                        Name = (string)e1.Element("Name"),
                                    })
                                    .ToArray(),
                        });
            foreach (var project in projects)
            {
                Projects.Add(project);
                foreach (var task in project.Tasks)
                {
                    task.Parent = project;
                    Tasks.Add(task);
                }
            }

        }
    }
}
