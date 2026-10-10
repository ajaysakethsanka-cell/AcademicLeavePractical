using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcademicLeavePractical
{
    public partial class LeaveApplication : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Calendar starts on today's date
                cldLeave.SelectedDate = DateTime.Today;
                cldLeave.VisibleDate = DateTime.Today;

                // ---------- COOKIE READ: remember the name from last time ----------
                HttpCookie nameCookie = Request.Cookies["LeaveName"];
                if (nameCookie != null)
                {
                    txtName.Text = nameCookie.Value;
                }

                // ---------- COOKIE READ + WRITE: last visit time ----------
                HttpCookie visitCookie = Request.Cookies["LastVisit"];
                if (visitCookie != null)
                {
                    lblInfo.Text = "Last visit: " + Server.UrlDecode(visitCookie.Value);
                }
                HttpCookie newVisit = new HttpCookie("LastVisit", Server.UrlEncode(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt")));
                newVisit.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(newVisit);
            }
        }

        // ---------- RICH CONTROL EVENT: mark Sundays so they look different ----------
        protected void cldLeave_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Cell.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();

            if (name == "" || txtReason.Text.Trim() == "")
            {
                lblResult.Text = "<span style='color:red'>Please enter Name and Reason.</span>";
                return;
            }

            // Show the result (HtmlEncode keeps user text safe)
            lblResult.Text =
                "<b>Leave Request Submitted</b><br />" +
                "Name: " + Server.HtmlEncode(name) + "<br />" +
                "Leave Date: " + cldLeave.SelectedDate.ToString("dd-MMM-yyyy") + "<br />" +
                "Leave Type: " + Server.HtmlEncode(ddlType.SelectedValue) + "<br />" +
                "Reason: " + Server.HtmlEncode(txtReason.Text.Trim());

            // ---------- SESSION: count requests made in this browser session ----------
            int count = 0;
            if (Session["RequestCount"] != null)
            {
                count = (int)Session["RequestCount"];
            }
            count++;
            Session["RequestCount"] = count;
            lblInfo.Text = "Requests submitted in this session: " + count;

            // ---------- COOKIE WRITE: remember the name for next visit ----------
            HttpCookie nameCookie = new HttpCookie("LeaveName", name);
            nameCookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(nameCookie);
        }
    }
}
