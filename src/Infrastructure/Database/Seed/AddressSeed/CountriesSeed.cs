using Domain.Countries;
using Domain.Districts;
using Domain.Regions;
using Domain.SubDistricts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed.AddressSeed;

internal static class CountriesSeed
{
    private static readonly (
        string Country,
        (string Region, (string District, string[] SubDistricts)[] Districts)[] Regions
    )[] Data =
    [
        (
            "Bangladesh",
            [
                (
                    "Dhaka",
                    [
                        ("Dhaka", ["Dhamrai", "Dohar", "Keraniganj", "Nawabganj", "Savar"]),
                        (
                            "Faridpur",
                            [
                                "Alfadanga",
                                "Bhanga",
                                "Boalmari",
                                "Charbhadrasan",
                                "Faridpur Sadar",
                                "Madhukhali",
                                "Nagarkanda",
                                "Sadarpur",
                                "Saltha",
                            ]
                        ),
                        (
                            "Gazipur",
                            ["Gazipur Sadar", "Kaliakair", "Kaliganj", "Kapasia", "Sreepur"]
                        ),
                        (
                            "Gopalganj",
                            ["Gopalganj Sadar", "Kashiani", "Kotalipara", "Muksudpur", "Tungipara"]
                        ),
                        (
                            "Kishoreganj",
                            [
                                "Austagram",
                                "Bajitpur",
                                "Bhairab",
                                "Hossainpur",
                                "Itna",
                                "Karimganj",
                                "Katiadi",
                                "Kishoreganj Sadar",
                                "Kuliarchar",
                                "Mithamain",
                                "Nikli",
                                "Pakundia",
                                "Tarail",
                            ]
                        ),
                        ("Madaripur", ["Rajoir", "Madaripur Sadar", "Kalkini", "Shibchar"]),
                        (
                            "Manikganj",
                            [
                                "Daulatpur",
                                "Ghior",
                                "Harirampur",
                                "Manikgonj Sadar",
                                "Saturia",
                                "Shivalaya",
                                "Singair",
                            ]
                        ),
                        (
                            "Munshiganj",
                            [
                                "Gazaria",
                                "Lohajang",
                                "Munshiganj Sadar",
                                "Sirajdikhan",
                                "Sreenagar",
                                "Tongibari",
                            ]
                        ),
                        (
                            "Narayanganj",
                            ["Araihazar", "Bandar", "Narayanganj Sadar", "Rupganj", "Sonargaon"]
                        ),
                        (
                            "Narsingdi",
                            [
                                "Belabo",
                                "Monohardi",
                                "Narsingdi Sadar",
                                "Palash",
                                "Raipura",
                                "Shibpur",
                            ]
                        ),
                        (
                            "Rajbari",
                            ["Baliakandi", "Goalandaghat", "Kalukhali", "Pangsha", "Rajbari Sadar"]
                        ),
                        (
                            "Shariatpur",
                            [
                                "Bhedarganj",
                                "Damudya",
                                "Gosairhat",
                                "Naria",
                                "Shariatpur Sadar",
                                "Zajira",
                            ]
                        ),
                        (
                            "Tangail",
                            [
                                "Basail",
                                "Bhuapur",
                                "Delduar",
                                "Dhanbari",
                                "Ghatail",
                                "Gopalpur",
                                "Kalihati",
                                "Madhupur",
                                "Mirzapur",
                                "Nagarpur",
                                "Sakhipur",
                                "Tangail Sadar",
                            ]
                        ),
                    ]
                ),
                (
                    "Chittagong",
                    [
                        (
                            "Bandarban",
                            [
                                "Ali Kadam",
                                "Bandarban Sadar",
                                "Lama",
                                "Naikhongchhari",
                                "Rowangchhari",
                                "Ruma",
                                "Thanchi",
                            ]
                        ),
                        (
                            "Brahmanbaria",
                            [
                                "Akhaura",
                                "Ashuganj",
                                "Bancharampur",
                                "Brahmanbaria Sadar",
                                "Bijoynagar",
                                "Kasba",
                                "Nabinagar",
                                "Nasirnagar",
                                "Sarail",
                            ]
                        ),
                        (
                            "Chandpur",
                            [
                                "Chandpur Sadar",
                                "Faridganj",
                                "Haimchar",
                                "Haziganj",
                                "Kachua",
                                "Matlab Dakshin",
                                "Matlab Uttar",
                                "Shahrasti",
                            ]
                        ),
                        (
                            "Chattogram",
                            [
                                "Anwara",
                                "Banshkhali",
                                "Boalkhali",
                                "Chandanaish",
                                "Fatikchhari",
                                "Hathazari",
                                "Karnaphuli",
                                "Lohagara",
                                "Mirsharai",
                                "Patiya",
                                "Rangunia",
                                "Raozan",
                                "Sandwip",
                                "Satkania",
                                "Sitakunda",
                            ]
                        ),
                        (
                            "Cox's Bazar",
                            [
                                "Chakaria",
                                "Cox's Bazar Sadar",
                                "Kutubdia",
                                "Maheshkhali",
                                "Ramu",
                                "Teknaf",
                                "Ukhia",
                                "Pekua",
                            ]
                        ),
                        (
                            "Cumilla",
                            [
                                "Barura",
                                "Brahmanpara",
                                "Burichang",
                                "Chandina",
                                "Chauddagram",
                                "Cumilla Adarsha Sadar",
                                "Cumilla Sadar Dakshin",
                                "Daudkandi",
                                "Debidwar",
                                "Homna",
                                "Laksam",
                                "Lalmai",
                                "Meghna",
                                "Monohargonj",
                                "Muradnagar",
                                "Nangalkot",
                                "Titas",
                            ]
                        ),
                        (
                            "Feni",
                            [
                                "Chhagalnaiya",
                                "Daganbhuiyan",
                                "Feni Sadar",
                                "Fulgazi",
                                "Parshuram",
                                "Sonagazi",
                            ]
                        ),
                        (
                            "Khagrachari",
                            [
                                "Dighinala",
                                "Khagrachhari Sadar",
                                "Lakshmichhari",
                                "Mahalchhari",
                                "Manikchhari",
                                "Matiranga",
                                "Panchhari",
                                "Ramgarh",
                            ]
                        ),
                        (
                            "Lakshmipur",
                            ["Lakshmipur Sadar", "Kamalnagar", "Raipur", "Ramganj", "Ramgati"]
                        ),
                        (
                            "Noakhali",
                            [
                                "Begumganj",
                                "Noakhali Sadar",
                                "Chatkhil",
                                "Companiganj",
                                "Hatiya",
                                "Kabirhat",
                                "Senbagh",
                                "Sonaimuri",
                                "Subarnachar",
                            ]
                        ),
                        (
                            "Rangamati",
                            [
                                "Bagaichhari",
                                "Barkal",
                                "Belaichhari",
                                "Juraichhari",
                                "Kaptai",
                                "Kawkhali",
                                "Langadu",
                                "Naniyachar",
                                "Rajasthali",
                                "Rangamati Sadar",
                            ]
                        ),
                    ]
                ),
                (
                    "Rajshahi",
                    [
                        (
                            "Bogura",
                            [
                                "Adamdighi",
                                "Bogura Sadar",
                                "Dhunat",
                                "Dhupchanchia",
                                "Gabtali",
                                "Kahaloo",
                                "Nandigram",
                                "Sariakandi",
                                "Shajahanpur",
                                "Sherpur",
                                "Shibganj",
                                "Sonatola",
                            ]
                        ),
                        (
                            "Joypurhat",
                            ["Akkelpur", "Joypurhat Sadar", "Kalai", "Khetlal", "Panchbibi"]
                        ),
                        (
                            "Naogaon",
                            [
                                "Atrai",
                                "Badalgachhi",
                                "Manda",
                                "Dhamoirhat",
                                "Mohadevpur",
                                "Naogaon Sadar",
                                "Niamatpur",
                                "Patnitala",
                                "Porsha",
                                "Raninagar",
                                "Sapahar",
                            ]
                        ),
                        (
                            "Natore",
                            [
                                "Bagatipara",
                                "Baraigram",
                                "Gurudaspur",
                                "Lalpur",
                                "Natore Sadar",
                                "Singra",
                                "Naldanga",
                            ]
                        ),
                        (
                            "Chapai Nawabganj",
                            [
                                "Bholahat",
                                "Gomastapur",
                                "Nachole",
                                "Chapai Nawabganj Sadar",
                                "Shibganj",
                            ]
                        ),
                        (
                            "Pabna",
                            [
                                "Atgharia",
                                "Bera",
                                "Bhangura",
                                "Chatmohar",
                                "Faridpur",
                                "Ishwardi",
                                "Pabna Sadar",
                                "Santhia",
                                "Sujanagar",
                            ]
                        ),
                        (
                            "Rajshahi",
                            [
                                "Bagha",
                                "Bagmara",
                                "Charghat",
                                "Durgapur",
                                "Godagari",
                                "Mohanpur",
                                "Paba",
                                "Puthia",
                                "Tanore",
                            ]
                        ),
                        (
                            "Sirajganj",
                            [
                                "Belkuchi",
                                "Chauhali",
                                "Kamarkhanda",
                                "Kazipur",
                                "Raiganj",
                                "Shahjadpur",
                                "Sirajganj Sadar",
                                "Tarash",
                                "Ullahpara",
                            ]
                        ),
                    ]
                ),
                (
                    "Rangpur",
                    [
                        (
                            "Dinajpur",
                            [
                                "Birampur",
                                "Birganj",
                                "Biral",
                                "Bochaganj",
                                "Chirirbandar",
                                "Dinajpur Sadar",
                                "Ghoraghat",
                                "Hakimpur",
                                "Kaharole",
                                "Khansama",
                                "Nawabganj",
                                "Parbatipur",
                                "Phulbari",
                            ]
                        ),
                        (
                            "Gaibandha",
                            [
                                "Phulchhari",
                                "Gaibandha Sadar",
                                "Gobindaganj",
                                "Palashbari",
                                "Sadullapur",
                                "Sughatta",
                                "Sundarganj",
                            ]
                        ),
                        (
                            "Kurigram",
                            [
                                "Bhurungamari",
                                "Char Rajibpur",
                                "Chilmari",
                                "Kurigram Sadar",
                                "Nageshwari",
                                "Phulbari",
                                "Rajarhat",
                                "Raomari",
                                "Ulipur",
                            ]
                        ),
                        (
                            "Lalmonirhat",
                            ["Aditmari", "Hatibandha", "Kaliganj", "Lalmonirhat Sadar", "Patgram"]
                        ),
                        (
                            "Nilphamari",
                            [
                                "Dimla",
                                "Domar",
                                "Jaldhaka",
                                "Kishoreganj",
                                "Nilphamari Sadar",
                                "Saidpur",
                            ]
                        ),
                        (
                            "Panchagarh",
                            ["Atwari", "Boda", "Debiganj", "Panchagarh Sadar", "Tetulia"]
                        ),
                        (
                            "Rangpur",
                            [
                                "Badarganj",
                                "Gangachhara",
                                "Kaunia",
                                "Mithapukur",
                                "Pirgachha",
                                "Pirganj",
                                "Rangpur Sadar",
                                "Taraganj",
                            ]
                        ),
                        (
                            "Thakurgaon",
                            ["Baliadangi", "Haripur", "Pirganj", "Ranisankail", "Thakurgaon Sadar"]
                        ),
                    ]
                ),
                (
                    "Sylhet",
                    [
                        (
                            "Habiganj",
                            [
                                "Ajmiriganj",
                                "Bahubal",
                                "Baniyachong",
                                "Chunarughat",
                                "Habiganj Sadar",
                                "Lakhai",
                                "Madhabpur",
                                "Nabiganj",
                                "Sayestaganj",
                            ]
                        ),
                        (
                            "Moulvibazar",
                            [
                                "Barlekha",
                                "Juri",
                                "Kamalganj",
                                "Kulaura",
                                "Moulvibazar Sadar",
                                "Rajnagar",
                                "Sreemangal",
                            ]
                        ),
                        (
                            "Sunamganj",
                            [
                                "Bishwamvarpur",
                                "Chhatak",
                                "Dakshin Sunamganj",
                                "Derai",
                                "Dharamapasha",
                                "Dowarabazar",
                                "Jagannathpur",
                                "Jamalganj",
                                "Sullah",
                                "Sunamganj Sadar",
                                "Tahirpur",
                            ]
                        ),
                        (
                            "Sylhet",
                            [
                                "Balaganj",
                                "Beanibazar",
                                "Bishwanath",
                                "Companigonj",
                                "Dakshin Surma",
                                "Fenchuganj",
                                "Golapganj",
                                "Gowainghat",
                                "Jaintiapur",
                                "Kanaighat",
                                "Osmani Nagar",
                                "Sylhet Sadar",
                                "Zakiganj",
                            ]
                        ),
                    ]
                ),
                (
                    "Mymensingh",
                    [
                        (
                            "Jamalpur",
                            [
                                "Baksiganj",
                                "Dewanganj",
                                "Islampur",
                                "Jamalpur Sadar",
                                "Madarganj",
                                "Melandaha",
                                "Sarishabari",
                            ]
                        ),
                        (
                            "Mymensingh",
                            [
                                "Bhaluka",
                                "Dhobaura",
                                "Fulbaria",
                                "Gafargaon",
                                "Gauripur",
                                "Haluaghat",
                                "Ishwarganj",
                                "Mymensingh Sadar",
                                "Muktagachha",
                                "Nandail",
                                "Phulpur",
                                "Tara Khanda",
                                "Trishal",
                            ]
                        ),
                        (
                            "Netrokona",
                            [
                                "Atpara",
                                "Barhatta",
                                "Durgapur",
                                "Khaliajuri",
                                "Kalmakanda",
                                "Kendua",
                                "Madan",
                                "Mohanganj",
                                "Netrokona Sadar",
                                "Purbadhala",
                            ]
                        ),
                        (
                            "Sherpur",
                            ["Jhenaigati", "Nakla", "Nalitabari", "Sherpur Sadar", "Sreebardi"]
                        ),
                    ]
                ),
                (
                    "Khulna",
                    [
                        (
                            "Bagerhat",
                            [
                                "Bagerhat Sadar",
                                "Chitalmari",
                                "Fakirhat",
                                "Kachua",
                                "Mollahat",
                                "Mongla",
                                "Morrelganj",
                                "Rampal",
                                "Sarankhola",
                            ]
                        ),
                        ("Chuadanga", ["Alamdanga", "Chuadanga Sadar", "Damurhuda", "Jibannagar"]),
                        (
                            "Jashore",
                            [
                                "Abhaynagar",
                                "Bagherpara",
                                "Chaugachha",
                                "Jhikargachha",
                                "Keshabpur",
                                "Jashore Sadar",
                                "Manirampur",
                                "Sharsha",
                            ]
                        ),
                        (
                            "Jhenaidah",
                            [
                                "Harinakunda",
                                "Jhenaidah Sadar",
                                "Kaliganj",
                                "Kotchandpur",
                                "Maheshpur",
                                "Shailkupa",
                            ]
                        ),
                        (
                            "Khulna",
                            [
                                "Batiaghata",
                                "Dacope",
                                "Dumuria",
                                "Dighalia",
                                "Koyra",
                                "Paikgachha",
                                "Phultala",
                                "Rupsha",
                                "Terokhada",
                            ]
                        ),
                        (
                            "Kushtia",
                            [
                                "Bheramara",
                                "Daulatpur",
                                "Khoksa",
                                "Kumarkhali",
                                "Kushtia Sadar",
                                "Mirpur",
                            ]
                        ),
                        ("Magura", ["Magura Sadar", "Mohammadpur", "Shalikha", "Sreepur"]),
                        ("Meherpur", ["Gangni", "Meherpur Sadar", "Mujibnagar"]),
                        ("Narail", ["Kalia", "Lohagara", "Narail Sadar"]),
                        (
                            "Satkhira",
                            [
                                "Assasuni",
                                "Debhata",
                                "Kalaroa",
                                "Kaliganj",
                                "Satkhira Sadar",
                                "Shyamnagar",
                                "Tala",
                            ]
                        ),
                    ]
                ),
                (
                    "Barishal",
                    [
                        (
                            "Barguna",
                            ["Amtali", "Bamna", "Barguna Sadar", "Betagi", "Patharghata", "Taltali"]
                        ),
                        (
                            "Barishal",
                            [
                                "Agailjhara",
                                "Babuganj",
                                "Bakerganj",
                                "Banaripara",
                                "Barisal Sadar",
                                "Gaurnadi",
                                "Hizla",
                                "Mehendiganj",
                                "Muladi",
                                "Wazirpur",
                            ]
                        ),
                        (
                            "Bhola",
                            [
                                "Bhola Sadar",
                                "Burhanuddin",
                                "Char Fasson",
                                "Daulatkhan",
                                "Lalmohan",
                                "Manpura",
                                "Tazumuddin",
                            ]
                        ),
                        ("Jhalokati", ["Kathalia", "Nalchhiti", "Jhalokati Sadar", "Rajapur"]),
                        (
                            "Patuakhali",
                            [
                                "Bauphal",
                                "Dashmina",
                                "Dumki",
                                "Galachipa",
                                "Kalapara",
                                "Mirzaganj",
                                "Patuakhali Sadar",
                                "Rangabali",
                            ]
                        ),
                        (
                            "Pirojpur",
                            [
                                "Bhandaria",
                                "Indurkani",
                                "Kawkhali",
                                "Mathbaria",
                                "Nazirpur",
                                "Nesarabad (Swarupkati)",
                                "Pirojpur Sadar",
                            ]
                        ),
                    ]
                ),
            ]
        ),
    ];

    internal static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(Countries());
        modelBuilder.Entity<Region>().HasData(Regions());
        modelBuilder.Entity<District>().HasData(Districts());
        modelBuilder.Entity<SubDistrict>().HasData(SubDistricts());
    }

    private static Country[] Countries() =>
        Data.Select(x => new Country
            {
                Id = SeedIds.Country(x.Country),
                Name = x.Country,
                Capital = "",
                PhoneCode = "",
                IsNew = false,
            })
            .ToArray();

    private static Region[] Regions() =>
        Data.SelectMany(x =>
                x.Regions.Select(r => new Region
                {
                    Id = SeedIds.Region(x.Country, r.Region),
                    CountryId = SeedIds.Country(x.Country),
                    Name = r.Region,
                    IsNew = false,
                })
            )
            .ToArray();

    private static District[] Districts() =>
        Data.SelectMany(x =>
                x.Regions.SelectMany(r =>
                    r.Districts.Select(d => new District
                    {
                        Id = SeedIds.District(x.Country, r.Region, d.District),
                        RegionId = SeedIds.Region(x.Country, r.Region),
                        Name = d.District,
                        IsNew = false,
                    })
                )
            )
            .ToArray();

    private static SubDistrict[] SubDistricts() =>
        Data.SelectMany(x =>
                x.Regions.SelectMany(r =>
                    r.Districts.SelectMany(d =>
                        d.SubDistricts.Select(sd => new SubDistrict
                        {
                            Id = SeedIds.SubDistrict(x.Country, r.Region, d.District, sd),
                            DistrictId = SeedIds.District(x.Country, r.Region, d.District),
                            Name = sd,
                            IsNew = false,
                        })
                    )
                )
            )
            .ToArray();
}
