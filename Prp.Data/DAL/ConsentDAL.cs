using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class ConsentDAL : PrpDBConnect
	{
		public ConsentDAL()
		{
		}

		public Message AddUpdate(Consent obj)
		{
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spConsentAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@roundId", obj.roundId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@consentTypeId", obj.consentTypeId);
            sqlCommand.Parameters.AddWithValue("@otp", obj.otp);
            sqlCommand.Parameters.AddWithValue("@img", obj.img);
            sqlCommand.Parameters.AddWithValue("@mobileNumber", obj.mobileNumber);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public List<Consent> GetAllByApplicant(int applicantId)
		{
			List<Consent> consents = new List<Consent>();
			try
			{
				//List<spConsentGetByApplicant_Result> list = this.db.spConsentGetByApplicant(new int?(applicantId)).ToList<spConsentGetByApplicant_Result>();
				//consents = MapConsent.ToEntityList(list);
			}
			catch (Exception exception)
			{
				consents = new List<Consent>();
			}
			return consents;
		}

		public Consent GetByApplicant(int applicantId)
		{
			Consent consent = new Consent();
			try
			{
				//spConsentGetByApplicant_Result spConsentGetByApplicantResult = this.db.spConsentGetByApplicant(new int?(applicantId)).FirstOrDefault<spConsentGetByApplicant_Result>();
				//consent = MapConsent.ToEntity(spConsentGetByApplicantResult);
			}
			catch (Exception exception)
			{
			}
			return consent;
		}

		public Consent GetById(int consentId)
		{
			Consent consent = new Consent();
			try
			{
				tblConsent _tblConsent = this.db.tblConsents.FirstOrDefault<tblConsent>((tblConsent x) => x.consentId == consentId);
				consent = (_tblConsent == null ? new Consent() : MapConsent.ToEntity(_tblConsent));
			}
			catch (Exception exception)
			{
				consent = new Consent();
			}
			return consent;
		}

		public List<EntityDDL> GetTypeApplicantHasMerit(int applicantId, int round)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spGetTypeApplicantHasMerit_Result> list = this.db.spGetTypeApplicantHasMerit(new int?(applicantId), new int?(round)).ToList<spGetTypeApplicantHasMerit_Result>();
				entityDDLs = MapConsent.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}
	}
}