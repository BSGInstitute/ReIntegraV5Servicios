using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    internal class AulaVirtualContext : DbContext
    {
        public AulaVirtualContext()
        {
        }
        public AulaVirtualContext(DbContextOptions<AulaVirtualContext> options) : base(options)
        {
        }
        public virtual DbSet<MdlEnrol> MdlEnrol { get; set; }
        public virtual DbSet<MdlQuestionnaire> MdlQuestionnaire { get; set; }
        public virtual DbSet<MdlQuestionnaireQuestion> MdlQuestionnaireQuestion { get; set; }
        public virtual DbSet<MdlQuestionnaireResponse> MdlQuestionnaireResponse { get; set; }
        public virtual DbSet<MdlQuestionnaireResponseBool> MdlQuestionnaireResponseBool { get; set; }
        public virtual DbSet<MdlQuestionnaireResponseText> MdlQuestionnaireResponseText { get; set; }
        public virtual DbSet<MdlQuestionnaireSurvey> MdlQuestionnaireSurvey { get; set; }
        public virtual DbSet<MdlUser> MdlUser { get; set; }
        public virtual DbSet<MdlUserEnrolments> MdlUserEnrolments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=159.65.168.31;port=3306;user=bsgrupov_userdbb;password=BSgrup02015;database=bsgrupov_moodle_db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<MdlEnrol>(entity =>
            {
                entity.ToTable("mdl_enrol", "bsgrupov_moodle_db");

                entity.HasIndex(e => e.Courseid)
                    .HasName("mdl_enro_cou_ix");

                entity.HasIndex(e => e.Enrol)
                    .HasName("mdl_enro_enr_ix");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Courseid)
                    .HasColumnName("courseid")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Customchar1)
                    .HasColumnName("customchar1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Customchar2)
                    .HasColumnName("customchar2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Customchar3)
                    .HasColumnName("customchar3")
                    .HasMaxLength(1333)
                    .IsUnicode(false);

                entity.Property(e => e.Customdec1)
                    .HasColumnName("customdec1")
                    .HasColumnType("decimal(12,7)");

                entity.Property(e => e.Customdec2)
                    .HasColumnName("customdec2")
                    .HasColumnType("decimal(12,7)");

                entity.Property(e => e.Customint1)
                    .HasColumnName("customint1")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customint2)
                    .HasColumnName("customint2")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customint3)
                    .HasColumnName("customint3")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customint4)
                    .HasColumnName("customint4")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customint5)
                    .HasColumnName("customint5")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customint6)
                    .HasColumnName("customint6")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customint7)
                    .HasColumnName("customint7")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customint8)
                    .HasColumnName("customint8")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Customtext1)
                    .HasColumnName("customtext1")
                    .HasColumnType("longtext");

                entity.Property(e => e.Customtext2)
                    .HasColumnName("customtext2")
                    .HasColumnType("longtext");

                entity.Property(e => e.Customtext3)
                    .HasColumnName("customtext3")
                    .HasColumnType("longtext");

                entity.Property(e => e.Customtext4)
                    .HasColumnName("customtext4")
                    .HasColumnType("longtext");

                entity.Property(e => e.Enrol)
                    .IsRequired()
                    .HasColumnName("enrol")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Enrolenddate)
                    .HasColumnName("enrolenddate")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Enrolperiod)
                    .HasColumnName("enrolperiod")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Enrolstartdate)
                    .HasColumnName("enrolstartdate")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Expirynotify)
                    .HasColumnName("expirynotify")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Expirythreshold)
                    .HasColumnName("expirythreshold")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Notifyall)
                    .HasColumnName("notifyall")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Roleid)
                    .HasColumnName("roleid")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sortorder)
                    .HasColumnName("sortorder")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timecreated)
                    .HasColumnName("timecreated")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timemodified)
                    .HasColumnName("timemodified")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<MdlQuestionnaire>(entity =>
            {
                entity.ToTable("mdl_questionnaire", "bsgrupov_moodle_db");

                entity.HasIndex(e => e.Sid)
                    .HasName("mdl_ques_sid_ix");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Autonum)
                    .HasColumnName("autonum")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("3");

                entity.Property(e => e.Closedate)
                    .HasColumnName("closedate")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Completionsubmit)
                    .HasColumnName("completionsubmit")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Course)
                    .HasColumnName("course")
                    .HasColumnType("bigint(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Intro)
                    .IsRequired()
                    .HasColumnName("intro")
                    .HasColumnType("longtext");

                entity.Property(e => e.Introformat)
                    .HasColumnName("introformat")
                    .HasColumnType("smallint(4)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Navigate)
                    .HasColumnName("navigate")
                    .HasColumnType("tinyint(2)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Opendate)
                    .HasColumnName("opendate")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Qtype)
                    .HasColumnName("qtype")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RespEligible)
                    .IsRequired()
                    .HasColumnName("resp_eligible")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("all");

                entity.Property(e => e.RespView)
                    .HasColumnName("resp_view")
                    .HasColumnType("tinyint(2)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Respondenttype)
                    .IsRequired()
                    .HasColumnName("respondenttype")
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasDefaultValueSql("fullname");

                entity.Property(e => e.Resume)
                    .HasColumnName("resume")
                    .HasColumnType("tinyint(2)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Sid)
                    .HasColumnName("sid")
                    .HasColumnType("bigint(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timemodified)
                    .HasColumnName("timemodified")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<MdlQuestionnaireQuestion>(entity =>
            {
                entity.ToTable("mdl_questionnaire_question", "bsgrupov_moodle_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("longtext");

                entity.Property(e => e.Deleted)
                    .IsRequired()
                    .HasColumnName("deleted")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("n");

                entity.Property(e => e.Dependchoice)
                    .HasColumnName("dependchoice")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Dependquestion)
                    .HasColumnName("dependquestion")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("bigint(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Precise)
                    .HasColumnName("precise")
                    .HasColumnType("bigint(11)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Required)
                    .IsRequired()
                    .HasColumnName("required")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("n");

                entity.Property(e => e.ResultId)
                    .HasColumnName("result_id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.SurveyId)
                    .HasColumnName("survey_id")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<MdlQuestionnaireResponse>(entity =>
            {
                entity.ToTable("mdl_questionnaire_response", "bsgrupov_moodle_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Complete)
                    .IsRequired()
                    .HasColumnName("complete")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("n");

                entity.Property(e => e.Grade)
                    .HasColumnName("grade")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Submitted)
                    .HasColumnName("submitted")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SurveyId)
                    .HasColumnName("survey_id")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MdlQuestionnaireResponseBool>(entity =>
            {
                entity.ToTable("mdl_questionnaire_response_bool", "bsgrupov_moodle_db");

                entity.HasIndex(e => new { e.ResponseId, e.QuestionId })
                    .HasName("mdl_quesrespbool_resque_ix");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(11)");

                entity.Property(e => e.ChoiceId)
                    .IsRequired()
                    .HasColumnName("choice_id")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("y");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("question_id")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ResponseId)
                    .HasColumnName("response_id")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<MdlQuestionnaireResponseText>(entity =>
            {
                entity.ToTable("mdl_questionnaire_response_text", "bsgrupov_moodle_db");

                entity.HasIndex(e => new { e.ResponseId, e.QuestionId })
                    .HasName("mdl_quesresptext_resque_ix");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(11)");

                entity.Property(e => e.QuestionId)
                    .HasColumnName("question_id")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Response)
                    .HasColumnName("response")
                    .HasColumnType("longtext");

                entity.Property(e => e.ResponseId)
                    .HasColumnName("response_id")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");
            });

            modelBuilder.Entity<MdlQuestionnaireSurvey>(entity =>
            {
                entity.ToTable("mdl_questionnaire_survey", "bsgrupov_moodle_db");

                entity.HasIndex(e => e.Name)
                    .HasName("mdl_quessurv_nam_ix");

                entity.HasIndex(e => e.Owner)
                    .HasName("mdl_quessurv_own_ix");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Info)
                    .HasColumnName("info")
                    .HasColumnType("longtext");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasColumnName("owner")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Realm)
                    .IsRequired()
                    .HasColumnName("realm")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Subtitle)
                    .HasColumnName("subtitle")
                    .HasColumnType("longtext");

                entity.Property(e => e.ThankBody)
                    .HasColumnName("thank_body")
                    .HasColumnType("longtext");

                entity.Property(e => e.ThankHead)
                    .HasColumnName("thank_head")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ThanksPage)
                    .HasColumnName("thanks_page")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Theme)
                    .HasColumnName("theme")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MdlUser>(entity =>
            {
                entity.ToTable("mdl_user", "bsgrupov_moodle_db");

                entity.HasIndex(e => e.Auth)
                    .HasName("mdl_user_aut_ix");

                entity.HasIndex(e => e.City)
                    .HasName("mdl_user_cit_ix");

                entity.HasIndex(e => e.Confirmed)
                    .HasName("mdl_user_con_ix");

                entity.HasIndex(e => e.Country)
                    .HasName("mdl_user_cou_ix");

                entity.HasIndex(e => e.Deleted)
                    .HasName("mdl_user_del_ix");

                entity.HasIndex(e => e.Email)
                    .HasName("mdl_user_ema_ix");

                entity.HasIndex(e => e.Firstname)
                    .HasName("mdl_user_fir_ix");

                entity.HasIndex(e => e.Idnumber)
                    .HasName("mdl_user_idn_ix");

                entity.HasIndex(e => e.Lastaccess)
                    .HasName("mdl_user_las2_ix");

                entity.HasIndex(e => e.Lastname)
                    .HasName("mdl_user_las_ix");

                entity.HasIndex(e => new { e.Mnethostid, e.Username })
                    .HasName("mdl_user_mneuse_uix")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Aim)
                    .IsRequired()
                    .HasColumnName("aim")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Auth)
                    .IsRequired()
                    .HasColumnName("auth")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("manual");

                entity.Property(e => e.Autosubscribe)
                    .HasColumnName("autosubscribe")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Confirmed)
                    .HasColumnName("confirmed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Currentlogin)
                    .HasColumnName("currentlogin")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasColumnName("department")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("longtext");

                entity.Property(e => e.Descriptionformat)
                    .HasColumnName("descriptionformat")
                    .HasColumnType("tinyint(2)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Emailstop)
                    .HasColumnName("emailstop")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Firstaccess)
                    .HasColumnName("firstaccess")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Htmleditor)
                    .HasColumnName("htmleditor")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Icq)
                    .IsRequired()
                    .HasColumnName("icq")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Idnumber)
                    .IsRequired()
                    .HasColumnName("idnumber")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Imagealt)
                    .HasColumnName("imagealt")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Institution)
                    .IsRequired()
                    .HasColumnName("institution")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Lang)
                    .IsRequired()
                    .HasColumnName("lang")
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasDefaultValueSql("en");

                entity.Property(e => e.Lastaccess)
                    .HasColumnName("lastaccess")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lastip)
                    .IsRequired()
                    .HasColumnName("lastip")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Lastlogin)
                    .HasColumnName("lastlogin")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Maildigest)
                    .HasColumnName("maildigest")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Maildisplay)
                    .HasColumnName("maildisplay")
                    .HasColumnType("tinyint(2)")
                    .HasDefaultValueSql("2");

                entity.Property(e => e.Mailformat)
                    .HasColumnName("mailformat")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Mnethostid)
                    .HasColumnName("mnethostid")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Msn)
                    .IsRequired()
                    .HasColumnName("msn")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone1)
                    .IsRequired()
                    .HasColumnName("phone1")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone2)
                    .IsRequired()
                    .HasColumnName("phone2")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Picture)
                    .HasColumnName("picture")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Policyagreed)
                    .HasColumnName("policyagreed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Secret)
                    .IsRequired()
                    .HasColumnName("secret")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Skype)
                    .IsRequired()
                    .HasColumnName("skype")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Suspended)
                    .HasColumnName("suspended")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Theme)
                    .IsRequired()
                    .HasColumnName("theme")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Timecreated)
                    .HasColumnName("timecreated")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timemodified)
                    .HasColumnName("timemodified")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timezone)
                    .IsRequired()
                    .HasColumnName("timezone")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("99");

                entity.Property(e => e.Trackforums)
                    .HasColumnName("trackforums")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Trustbitmask)
                    .HasColumnName("trustbitmask")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Yahoo)
                    .IsRequired()
                    .HasColumnName("yahoo")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MdlUserEnrolments>(entity =>
            {
                entity.ToTable("mdl_user_enrolments", "bsgrupov_moodle_db");

                entity.HasIndex(e => e.Enrolid)
                    .HasName("mdl_userenro_enr_ix");

                entity.HasIndex(e => e.Modifierid)
                    .HasName("mdl_userenro_mod_ix");

                entity.HasIndex(e => e.Userid)
                    .HasName("mdl_userenro_use_ix");

                entity.HasIndex(e => new { e.Enrolid, e.Userid })
                    .HasName("mdl_userenro_enruse_uix")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Enrolid)
                    .HasColumnName("enrolid")
                    .HasColumnType("bigint(10)");

                entity.Property(e => e.Modifierid)
                    .HasColumnName("modifierid")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timecreated)
                    .HasColumnName("timecreated")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timeend)
                    .HasColumnName("timeend")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("2147483647");

                entity.Property(e => e.Timemodified)
                    .HasColumnName("timemodified")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Timestart)
                    .HasColumnName("timestart")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("bigint(10)");
            });

        }
    }
   
}
