<body>
  <h1>Projekat 1</h1>
  Aplikacija koja pribavlja podatke o pesmama zajedno sa njihovim tekstovima.
  Aplikacija radi u docker-u.
  <ul>Mikroservisi koji se koriste u ovom projektu su:
    <li>MicroService-Gateway</li>
    <li>MicroService-DB</li>
  </ul>
  <h2>Pokretanje aplikacije</h2>
   <ul>Koraci za pokretanje prvog projekta:
    <li>Locirati se u folder ~/Projekat1</li>
    <li>Izvrsiti komandu <pre>docker compose -f "docker-compose.yml" up -d --build</pre></li>
    <li>Za pokretanje servisa samo iz projekta 1 izvrsiti komandu <pre>docker compose -f "docker-compose.yml" up -d --build service-gateway service-db</pre></li>
  </ul>
  Otvoriti u browser-u adresu localhost:5170/swagger/index.html</br>
  Otvoreni prozor prikazuje swagger ui koji olaksava testiranje funkcionalnosti api funkcija.</br>
  Testirati funkciju <b>Get one</b> iz <b>Song</b> controller-a sa test podacima: AristName: "<b>Billie Eilish</b>", TrackName: "<b>bad guy</b>"</br>
  <hr>
  <h1>Projekat 2</h1>
  Aplikacija koja pribavlja podatke sa senzora o kvalitetu zemljista i vrsi analizu prikupljenih podataka. U slucaju loseg kvaliteta se obavestava klijent.
  Aplikacija radi u docker-u.
  <ul>Mikroservisi koji se koriste u ovom projektu su:
    <li>MicroService-Gateway</li>
    <li>MicroService-Analytics</li>
    <li>MicroService-Notification</li>
  </ul>
  <h2>Podesavanje eKuiper-a</h2>
  Pracenje upustva pogledati kod kolege <a href="https://github.com/sssteeefaaan/SOA-Projekat/tree/main/Projekat%20II"> Stefana Aleksica</a>.</br>
   <ul>Razlike u odnosu na Stefanov tutorijal znacajne za ovaj projekat:
    <li>Endpoint: <a href="http://kuiper:9081">http://kuiper:9081</a> </li>
    <li>MQTT name u Configuration tab-u mosquitto -> mqtt</li>
    <li>Server list: <a href="tcp://mqtt:1883">tcp://mqtt:1883</li>
    <li>Stream Name: <i>agricultureStream</i></li>
    <li>
      <ul>Stream fields: NOT DONE 
        <li><em>nitrogen - float</em></li>
        <li><em>phosphorus - float</em></li>
        <li><em>date - datetime</em></li>
        <li><em>precipitation - float</em></li>
        <li><em>wind - float</em></li>
        <li><em>weather - string</em></li>
  </ul>
  <h2>Pokretanje aplikacije</h2>
   <ul>Koraci za pokretanje drugog projekta:
    <li>Locirati se u folder ~/Projekat1</li>
    <li>Izvrsiti komandu <pre>docker compose -f "docker-compose.yml" up -d --build</pre></li>
    <li>Za pokretanje servisa samo iz projekta 2 izvrsiti komandu <pre>docker compose -f "docker-compose.yml" up -d --build service-gateway service-analytics service-notif</pre></li>
  </ul>
  Otvoriti u browser-u adresu localhost:5170/swagger/index.html</br>
  Otvoreni prozor prikazuje swagger ui koji olaksava testiranje funkcionalnosti api funkcija.</br>
  Testirati funkciju <b>Get one</b> iz <b>Song</b> controller-a sa test podacima: AristName: "<b>Billie Eilish</b>", TrackName: "<b>bad guy</b>"</br>
</body>
