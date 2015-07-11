using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//reference the ef models
using lesson9.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace lesson9
{
    public partial class course : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if save wasn't clicked AND we have a studentID in the URL
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                GetCourse();
            }

            using (comp2007Entities db = new comp2007Entities())
            {
                var objD = (from d in db.Departments
                            select new { d.DepartmentID, d.Name });

                ddDepartment.DataValueField = "DepartmentID";
                ddDepartment.DataTextField = "Name";
                ddDepartment.DataSource = objD.ToList();
                ddDepartment.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //use EF to connect to SQL server
            using (comp2007Entities db = new comp2007Entities())
            {
                //use the student model to save the new record
                Course c = new Course();
                Int32 CourseID = 0;
                //check the query string for an id so we can determine add / update
                if (Request.QueryString["CourseID"] != null)
                {
                    //get the ID from the URL
                    CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

                    //get the current student from Entity Framework
                    c = (from objS in db.Courses
                         where objS.CourseID == CourseID
                         select objS).FirstOrDefault();
                }
                c.Title= txtTitle.Text;
                c.Credits = Convert.ToInt32(txtCredits.Text);
                c.DepartmentID = Convert.ToInt32(ddDepartment.SelectedValue);

                //call add only if we have no student ID
                if (CourseID == 0)
                    db.Courses.Add(c);

                db.SaveChanges();
                //redirect to the updated students page
                Response.Redirect("courses.aspx");
            }
        }

        protected void GetCourse()
        {
            //populate form with existing student record
            Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

            //connect to db using entity framework
            using (comp2007Entities db = new comp2007Entities())
            {
                //populate a student instance with the studentID from the URL paramater
                Course c = (from objS in db.Courses
                                where objS.CourseID == CourseID
                                select objS).FirstOrDefault();

                //map the student properties to the form controls if we found a record

                txtTitle.Text = c.Title;
                txtCredits.Text = c.Credits.ToString();
                ddDepartment.SelectedValue = c.DepartmentID.ToString();


                //enrollments - this code goes in the same method that populates the student form but below the existing code that's already in GetStudent()               
                var objE = (from s in db.Students
                            join e in db.Enrollments on s.StudentID equals e.StudentID
                            join co in db.Courses on e.CourseID equals co.CourseID
                            where co.CourseID == CourseID
                            select new { s.LastName, s.FirstMidName, s.EnrollmentDate, co.CourseID});

                grdStudent.DataSource = objE.ToList();
                grdStudent.DataBind();
              

            }
        }

        protected void grdStudent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}