using SharedKernel;

namespace Domain.Projects;

public sealed class Project : Entity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string Domain { get; set; }

    public string RedirectUrl { get; set; }

    public ProjectStatus Status { get; set; }
}


// উদ্দেশ্য কী?
// 👉 প্রতিটি সেকেন্ডারি প্রজেক্ট(App) Auth Server-এ কোন Application হিসেবে রেজিস্টার্ড—ওটা সংরক্ষণ করে।
// গুরুত্বপূর্ণ কলাম ও উদ্দেশ্য:
// Column উদ্দেশ্য
// ..............................................
// Application_ID প্রতিটি Application-এর ইউনিক আইডি
// name    প্রকল্পের নাম
// domain কোন ডোমেইন থেকে এই অ্যাপ কল করবে
// client_id অ্যাপের Public Key(Auth Server-এ পরিচয় দেয়)
// client_secret অ্যাপের গোপন সিক্রেট(Auth Server যাচাই করে)
// redirect_url Login success হলে কোথায় redirect করবে
