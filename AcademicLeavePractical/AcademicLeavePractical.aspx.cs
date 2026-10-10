using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicLeavePractical
{
    public partial class AcademicLeavePractical : Page
    {
        // Sample academic holidays (no database needed)
        List<DateTime> holidays = new List<DateTime> {
            new DateTime(2026, 10, 2),
            new DateTime(2026, 11, 14),
            new DateTime(2026, 12, 25)
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            // SESSION CHECK: not logged in -> go to the login page
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            lblWelcome.Text = Session["UserName"].ToString();

            if (!IsPostBack)
            {
                txtName.Text = Session["UserName"].ToString();
                cldLeave.SelectedDate = DateTime.Today;
                cldLeave.VisibleDate = DateTime.Today;

                // COOKIE READ + WRITE: last visit time
                HttpCookie visitCookie = Request.Cookies["LastVisit"];
                if (visitCookie != null)
                {
                    lblInfo.Text = "Last visit: " + Server.UrlDecode(visitCookie.Value);
                }
                HttpCookie newVisit = new HttpCookie("LastVisit",
                    Server.UrlEncode(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt")));
                newVisit.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(newVisit);
            }

            BindGrid();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // SESSION CLEAR: log the user out
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        // RICH CONTROL EVENT: holidays red, Sundays red text
        protected void cldLeave_DayRender(object sender, DayRenderEventArgs e)
        {
            if (holidays.Contains(e.Day.Date.Date))
            {
                e.Cell.BackColor = System.Drawing.Color.Red;
                e.Cell.ForeColor = System.Drawing.Color.White;
                e.Cell.ToolTip = "Academic Holiday";
            }
            else if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Cell.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string reason = txtReason.Text.Trim();

            if (name == "" || reason == "")
            {
                lblResult.Text = "<span style='color:red'>Please enter Name and Reason.</span>";
                return;
            }

            lblResult.Text =
                "<b>Leave Request Submitted</b><br />" +
                "Name: " + Server.HtmlEncode(name) + "<br />" +
                "Leave Date: " + cldLeave.SelectedDate.ToString("dd-MMM-yyyy") + "<br />" +
                "Leave Type: " + Server.HtmlEncode(ddlType.SelectedValue) + "<br />" +
                "Reason: " + Server.HtmlEncode(reason);

            // SESSION: keep all requests for this user's visit
            List<LeaveEntry> leaves = Session["Leaves"] as List<LeaveEntry>;
            if (leaves == null) leaves = new List<LeaveEntry>();

            leaves.Add(new LeaveEntry
            {
                Name = name,
                Date = cldLeave.SelectedDate.ToString("dd-MMM-yyyy"),
                Type = ddlType.SelectedValue,
                Reason = reason
            });
            Session["Leaves"] = leaves;

            txtReason.Text = "";
            BindGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Session.Remove("Leaves");
            lblResult.Text = "";
            BindGrid();
        }

        void BindGrid()
        {
            List<LeaveEntry> leaves = Session["Leaves"] as List<LeaveEntry>;
            gvLeaves.DataSource = leaves;
            gvLeaves.DataBind();
            lblCount.Text = "Total requests submitted: " + (leaves == null ? 0 : leaves.Count);
        }
    }

    public class LeaveEntry
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
    }
}
