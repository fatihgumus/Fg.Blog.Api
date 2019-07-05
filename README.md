# database bilgisi:
Appsettings.json dosyasına blogConnectionString te servername girildiğinde projenin ilk başlatılmasında  mssql database tablo ve örnek datalar oluşacaktır. bu nedenle veritabanı scriptleri paylaşılmasına gerek kalmadı.

# :Projede kullanıdığınız tasarım desenleri hangileridir? Bu desenleri neden kullandınız?
Repository Pattern kullanıldı. Projenin isteri bu şekildeydi.

# :Kullandığınız teknoloji ve kütüphaneler hakkında daha önce tecrübeniz oldu mu? Tek tek yazabilir misiniz?
Entity Framework ile ilgili çalışmam daha önce olmuştu.
Net Core Framework ve ASP.NET Web API ile ilgili ilk çalışmamdır. Araştırma yaparak bu projeyi oluşturdum.

# :Daha geniş vaktiniz olsaydı projeye neler eklemek isterdiniz?
cache yapısı olarak redis kullanabilirdim. Loglamayı daha geniş kapsamlı yapardım.
ve bir arayüz oluştururdum. DB yapısını daha fazla detaylandırır ve daha düzenli yapardım.

# : Eklemek istediğiniz bir yorumunuz var mı? 
projenin tamamı postman üzerinden test edilmiştir.

kullanıcı işlemleri:  AuthController
blog işlemleri:       BlogsController
yorum işlemleri:      CommetsController
