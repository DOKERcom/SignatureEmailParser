namespace SignatureEmailParser.BusinessLogic.Constants
{
    public static class SqlConstants
    {
        public const string UNSPECIFIED = "Unspecified";

        public const string FREETEXT_SEARCH_QUERY_TEMPLATE = "SELECT TOP({0}) * FROM [dbo].[SocialMediaMappings] S WHERE CONTAINS(({1}), '{2}')";

        public const string FREETEXT_EXPLICIT_SEARCH_QUERY_TEMPLATE = "SELECT TOP({0}) * FROM [dbo].[SocialMediaMappings] S WHERE {1} ";

        //public const string IGNORE_IDS_STATEMENT_TEMPLATE = " AND LinkedIn NOT IN ({0})";

        public const string IGNORE_IDS_STATEMENT_TEMPLATE = " AND NOT EXISTS (SELECT 1 FROM [dbo].[SocialMediaMappings] M WHERE LINKEDIN IN ({0}) AND S.ID = M.ID)";

        public const string FILTER_SEARCH_QUERY_TEMPLATE =
            @"WITH filteredCTE AS
            (
                SELECT
                    s.[Id],s.[CreatedAt],[Title],s.[Name],[Surname],s.[Domain],s.[Email],[LinkedIn],s.[Twitter],[CurrentCompany],[PastCompany],
					[School],[ProfileLanguage],[ServiceCategories],s.[Locality],s.[Street],s.[Postcode],s.[IPAddress],[Position],[DataSourceType],
					s.[WhenUpdated],[CompanyId],[BirthDate],[Experience],[FacebookId],[FacebookUsername],[Gender],[InferredSalary],
                    [InferredYearsExperience],[JobTitle],[Language],[LocationGeo],[Mobile],[Phone],[Skills],[Summary],[WorkEmail],[LinkedInConnections],
					[BounceResponseType],[StatusType],[BirthYear],[ProfileImage],s.[CountryId],s.[IndustryId],s.[CityId],[CurrentCompanyId],
					[PastCompanyId],[ProfileImageMapping],[RegionId],[PositionId],[EmailStatus],[WorkEmailStatus],[LinkedInStatus],	[TwitterStatus],
					[FacebookStatus],[MobileStatus],[PhoneStatus],[LastPost],[LastPostDate],[SharedConnections],
                    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) RowNumber
                FROM [dbo].[SocialMediaMappings] s
                {0}
            )
            SELECT * FROM filteredCTE
            WHERE RowNumber > {1} AND RowNumber <= {2}";

        public const string FILTER_SEARCH_QUERY_TEMPLATE_WITH_HEADCOUNT =
            @"WITH filteredCTE AS
            (
                SELECT
                    s.[Id],s.[CreatedAt],[Title],s.[Name],[Surname],s.[Domain],s.[Email],[LinkedIn],s.[Twitter],[CurrentCompany],[PastCompany],
					[School],[ProfileLanguage],[ServiceCategories],s.[Locality],s.[Street],s.[Postcode],s.[IPAddress],[Position],[DataSourceType],
					s.[WhenUpdated],[CompanyId],[BirthDate],[Experience],[FacebookId],[FacebookUsername],[Gender],[InferredSalary],
                    [InferredYearsExperience],[JobTitle],[Language],[LocationGeo],[Mobile],[Phone],[Skills],[Summary],[WorkEmail],[LinkedInConnections],
					[BounceResponseType],[StatusType],[BirthYear],[ProfileImage],s.[CountryId],s.[IndustryId],s.[CityId],[CurrentCompanyId],
					[PastCompanyId],[ProfileImageMapping],[RegionId],[PositionId],[EmailStatus],[WorkEmailStatus],[LinkedInStatus],	[TwitterStatus],
					[FacebookStatus],[MobileStatus],[PhoneStatus],[LastPost],[LastPostDate],[SharedConnections],
                    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) RowNumber
                FROM [dbo].[SocialMediaMappings] s
                LEFT JOIN Companies c on c.Id = s.CompanyId 
                {0}
            )
            SELECT * FROM filteredCTE
            WHERE RowNumber > {1} AND RowNumber <= {2}";

        public const string FILTER_SEARCH_QUERY_TEMPLATE_TO_EXCLUDE_PROFILES =
            @";WITH filteredCTE AS
            (
                SELECT
                    s.[Id],s.[CreatedAt],[Title],s.[Name],[Surname],s.[Domain],s.[Email],[LinkedIn],s.[Twitter],[CurrentCompany],[PastCompany],
					[School],[ProfileLanguage],[ServiceCategories],s.[Locality],s.[Street],s.[Postcode],s.[IPAddress],[Position],[DataSourceType],
					s.[WhenUpdated],[CompanyId],[BirthDate],[Experience],[FacebookId],[FacebookUsername],[Gender],[InferredSalary],
                    [InferredYearsExperience],[JobTitle],[Language],[LocationGeo],[Mobile],[Phone],[Skills],[Summary],[WorkEmail],[LinkedInConnections],
					[BounceResponseType],[StatusType],[BirthYear],[ProfileImage],s.[CountryId],s.[IndustryId],s.[CityId],[CurrentCompanyId],
					[PastCompanyId],[ProfileImageMapping],[RegionId],[PositionId],[EmailStatus],[WorkEmailStatus],[LinkedInStatus],	[TwitterStatus],
					[FacebookStatus],[MobileStatus],[PhoneStatus],[LastPost],[LastPostDate],[SharedConnections],
                    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) RowNumber
                FROM [dbo].[SocialMediaMappings] s
                LEFT JOIN @TTable tt ON {3} = tt.Tid
                {0}
            )
            SELECT * FROM filteredCTE
            WHERE RowNumber > {1} AND RowNumber <= {2}";

        public const string FILTER_SEARCH_QUERY_TEMPLATE_TO_EXCLUDE_PROFILES_WITH_HEADCOUNT =
            @";WITH filteredCTE AS
            (
                SELECT
                    s.[Id],s.[CreatedAt],[Title],s.[Name],[Surname],s.[Domain],s.[Email],[LinkedIn],s.[Twitter],[CurrentCompany],[PastCompany],
					[School],[ProfileLanguage],[ServiceCategories],s.[Locality],s.[Street],s.[Postcode],s.[IPAddress],[Position],[DataSourceType],
					s.[WhenUpdated],[CompanyId],[BirthDate],[Experience],[FacebookId],[FacebookUsername],[Gender],[InferredSalary],
                    [InferredYearsExperience],[JobTitle],[Language],[LocationGeo],[Mobile],[Phone],[Skills],[Summary],[WorkEmail],[LinkedInConnections],
					[BounceResponseType],[StatusType],[BirthYear],[ProfileImage],s.[CountryId],s.[IndustryId],s.[CityId],[CurrentCompanyId],
					[PastCompanyId],[ProfileImageMapping],[RegionId],[PositionId],[EmailStatus],[WorkEmailStatus],[LinkedInStatus],	[TwitterStatus],
					[FacebookStatus],[MobileStatus],[PhoneStatus],[LastPost],[LastPostDate],[SharedConnections],
                    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) RowNumber
                FROM [dbo].[SocialMediaMappings] s
                LEFT JOIN @TTable tt ON {3} = tt.Tid
                LEFT JOIN Companies c on c.Id = s.CompanyId 
                {0}
            )
            SELECT * FROM filteredCTE
            WHERE RowNumber > {1} AND RowNumber <= {2}";

        public const string FILTER_WHERE_CLAUSE_SINGLE_EQUALS_ID_TEMPLATE = " = {0} AND ";

        public const string FILTER_WHERE_CLAUSE_MULTIPLE_EQUALS_ID_TEMPLATE = " IN ({0}) AND ";

        public const string FILTER_WHERE_CLAUSE_BETWEEN_TEMPLATE = " {0} LIKE '%{1}%' AND ";

        public const string FILTER_LIKE_WHERE_CLAUSE_BETWEEN_TEMPLATE = " {0} LIKE '%{1}%' AND ";

        // this was for freetextsearch which we need to use when freetext is back up
        //public const string FILTER_WHERE_CLAUSE_LIKE_TEMPLATE = " CONTAINS({0}, '{1}') AND ";

        public const string FILTER_WHERE_CLAUSE_NOT_NULL_TEMPLATE = " {0} IS NOT NULL AND ";

        public const string FB_PROFILES_TO_EXCLUDE_WHERE_CLAUSE_TEMPLATE = " FacebookUsername NOT IN ('{0}') ";

        public const string TWITTER_PROFILES_TO_EXCLUDE_WHERE_CLAUSE_TEMPLATE = " Twitter NOT IN ('{0}') ";

        public const string LINKEDIN_PROFILES_TO_EXCLUDE_WHERE_CLAUSE_TEMPLATE = " LinkedIN NOT IN ('{0}') ";
    }
}
