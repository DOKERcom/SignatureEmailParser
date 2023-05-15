using System;
using System.Collections.Generic;
using System.Text;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public class Industries
    {
        private static Dictionary<int, string> _industries = null;

        private Industries()
        {
        }

        public static Dictionary<int, string> GetIndustries()
        {
            Dictionary<int, string> industries = new Dictionary<int, string>();
            industries.Add(0, "Unspecified");
            industries.Add(1, "Accounting");
            industries.Add(2, "Airlines/aviation");
            industries.Add(3, "Alternative Dispute Resolution");
            industries.Add(4, "Alternative Medicine");
            industries.Add(5, "Animation");
            industries.Add(6, "Apparel & Fashion");
            industries.Add(7, "Architecture & Planning");
            industries.Add(8, "Arts And Crafts");
            industries.Add(9, "Automotive");
            industries.Add(10, "Aviation & Aerospace");
            industries.Add(11, "Banking");
            industries.Add(12, "Biotechnology");
            industries.Add(13, "Broadcast Media");
            industries.Add(14, "Building Materials");
            industries.Add(15, "Business Supplies And Equipment");
            industries.Add(16, "Capital Markets");
            industries.Add(17, "Chemicals");
            industries.Add(18, "Civic & Social Organization");
            industries.Add(19, "Civil Engineering");
            industries.Add(20, "Commercial Real Estate");
            industries.Add(21, "Computer & Network Security");
            industries.Add(22, "Computer Games");
            industries.Add(23, "Computer Hardware");
            industries.Add(24, "Computer Networking");
            industries.Add(25, "Computer Software");
            industries.Add(26, "Construction");
            industries.Add(27, "Consumer Electronics");
            industries.Add(28, "Consumer Goods");
            industries.Add(29, "Consumer Services");
            industries.Add(30, "Cosmetics");
            industries.Add(31, "Dairy");
            industries.Add(32, "Defense & Space");
            industries.Add(33, "Design");
            industries.Add(34, "Education Management");
            industries.Add(35, "E-learning");
            industries.Add(36, "Electrical/electronic Manufacturing");
            industries.Add(37, "Entertainment");
            industries.Add(38, "Environmental Services");
            industries.Add(39, "Events Services");
            industries.Add(40, "Executive Office");
            industries.Add(41, "Facilities Services");
            industries.Add(42, "Farming");
            industries.Add(43, "Financial Services");
            industries.Add(44, "Fine Art");
            industries.Add(45, "Fishery");
            industries.Add(46, "Food & Beverages");
            industries.Add(47, "Food Production");
            industries.Add(48, "Fund-raising");
            industries.Add(49, "Furniture");
            industries.Add(50, "Gambling & Casinos");
            industries.Add(51, "Glass; Ceramics; & Concrete");
            industries.Add(52, "Government Administration");
            industries.Add(53, "Government Relations");
            industries.Add(54, "Graphic Design");
            industries.Add(55, "Health; Wellness And Fitness");
            industries.Add(56, "Higher Education");
            industries.Add(57, "Hospital & Health Care");
            industries.Add(58, "Hospitality");
            industries.Add(59, "Human Resources");
            industries.Add(60, "Import And Export");
            industries.Add(61, "Individual & Family Services");
            industries.Add(62, "Industrial Automation");
            industries.Add(63, "Information Services");
            industries.Add(64, "Information Technology And Services");
            industries.Add(65, "Insurance");
            industries.Add(66, "International Affairs");
            industries.Add(67, "International Trade And Development");
            industries.Add(68, "Internet");
            industries.Add(69, "Investment Banking");
            industries.Add(70, "Investment Management");
            industries.Add(71, "Judiciary");
            industries.Add(72, "Law Enforcement");
            industries.Add(73, "Law Practice");
            industries.Add(74, "Legal Services");
            industries.Add(75, "Legislative Office");
            industries.Add(76, "Leisure; Travel; & Tourism");
            industries.Add(77, "Libraries");
            industries.Add(78, "Logistics And Supply Chain");
            industries.Add(79, "Luxury Goods & Jewelry");
            industries.Add(80, "Machinery");
            industries.Add(81, "Management Consulting");
            industries.Add(82, "Maritime");
            industries.Add(83, "Market Research");
            industries.Add(84, "Marketing And Advertising");
            industries.Add(85, "Mechanical Or Industrial Engineering");
            industries.Add(86, "Media Production");
            industries.Add(87, "Medical Devices");
            industries.Add(88, "Medical Practice");
            industries.Add(89, "Mental Health Care");
            industries.Add(90, "Military");
            industries.Add(91, "Mining & Metals");
            industries.Add(92, "Motion Pictures And Film");
            industries.Add(93, "Museums And Institutions");
            industries.Add(94, "Music");
            industries.Add(95, "Nanotechnology");
            industries.Add(96, "Newspapers");
            industries.Add(97, "Non-profit Organization Management");
            industries.Add(98, "Oil & Energy");
            industries.Add(99, "Online Media");
            industries.Add(100, "Outsourcing/offshoring");
            industries.Add(101, "Package/freight Delivery");
            industries.Add(102, "Packaging And Containers");
            industries.Add(103, "Paper & Forest Products");
            industries.Add(104, "Performing Arts");
            industries.Add(105, "Pharmaceuticals");
            industries.Add(106, "Philanthropy");
            industries.Add(107, "Photography");
            industries.Add(108, "Plastics");
            industries.Add(109, "Political Organization");
            industries.Add(110, "Primary/secondary Education");
            industries.Add(111, "Printing");
            industries.Add(112, "Professional Training & Coaching");
            industries.Add(113, "Program Development");
            industries.Add(114, "Public Policy");
            industries.Add(115, "Public Relations And Communications");
            industries.Add(116, "Public Safety");
            industries.Add(117, "Publishing");
            industries.Add(118, "Railroad Manufacture");
            industries.Add(119, "Ranching");
            industries.Add(120, "Real Estate");
            industries.Add(121, "Recreational Facilities And Services");
            industries.Add(122, "Religious Institutions");
            industries.Add(123, "Renewables & Environment");
            industries.Add(124, "Research");
            industries.Add(125, "Restaurants");
            industries.Add(126, "Retail");
            industries.Add(127, "Security And Investigations");
            industries.Add(128, "Semiconductors");
            industries.Add(129, "Shipbuilding");
            industries.Add(130, "Sporting Goods");
            industries.Add(131, "Sports");
            industries.Add(132, "Staffing And Recruiting");
            industries.Add(133, "Supermarkets");
            industries.Add(134, "Telecommunications");
            industries.Add(135, "Textiles");
            industries.Add(136, "Think Tanks");
            industries.Add(137, "Tobbaco");
            industries.Add(138, "Translation And Localization");
            industries.Add(139, "Transportation/trucking/railroad");
            industries.Add(140, "Utilities");
            industries.Add(141, "Venture Capital & Private Equity");
            industries.Add(142, "Veterinary");
            industries.Add(143, "Warehousing");
            industries.Add(144, "Wholesale");
            industries.Add(145, "Wine And Spirits");
            industries.Add(146, "Wireless");
            industries.Add(147, "Writing And Editing");

            return industries;
        }

        public static Dictionary<int, string> GetInstance()
        {
            if (_industries == null)
            {
                _industries = GetIndustries();
            }

            return _industries;
        }
    }
}
