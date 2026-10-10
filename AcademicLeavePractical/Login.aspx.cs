using System;
using System.Web;
using System.Web.UI;

namespace AcademicLeavePractical
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Already logged in? go straight to the leave page
                if (Session["UserName"] != null)
                {
                    Response.Redirect("AcademicLeavePractical.aspx");
                    return;
                }

                // COOKIE READ: fill the username if "Remember me" was used before
                HttpCookie rememberCookie = Request.Cookies["RememberUser"];
                if (rememberCookie != null)
                {
                    txtUser.Text = rememberCookie.Value;
                    chkRemember.Checked = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text;

            // Fixed sample users (no database needed)
            bool valid = (user == "admin" && pass == "admin123") ||
                         (user == "student" && pass == "student123");

            if (valid)
            {
                // SESSION WRITE: user is now logged in
                Session["UserName"] = user;

                // COOKIE WRITE: remember username for 7 days if ticked
                if (chkRemember.Checked)
                {
                    HttpCookie rememberCookie = new HttpCookie("RememberUser", user);
                    rememberCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(rememberCookie);
                }
                else
                {
                    HttpCookie clearCookie = new HttpCookie("RememberUser", "");
                    clearCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(clearCookie);
                }

                Response.Redirect("AcademicLeavePractical.aspx");
            }
            else
            {
                lblMsg.Text = "Invalid username or password.";
            }
        }
    }
}
