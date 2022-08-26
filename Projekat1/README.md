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
  Otvoriti u browser-u adresu <a href="http://localhost:5170/swagger/index.html">http://localhost:5170/swagger</a></br>
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
    <li>eKuiper & eKuiperManager</li>
    <li>Mosquitto MQTT</li>
  </ul>
  <h2>Podesavanje eKuiper-a</h2>
  Konfiguracija eKuiper-a:
  <ol>
    <li>U pretraživaču otvoriti eKuiperManager Web UI  [<a href="http://localhost:9082">http://localhost:9082</a>]</li>
    <li>Ulogovati se:
      <ul>
        <li><b>username:</b> <em>admin</em></li>
        <li><b>password:</b> <em>public</em></li>
      </ul>
    </li><br/>
    <li>Dodati novi servis:
      <ul>
        <li><b>Service type:</b> <em>Dirrect link service</em></li>
        <li><b>Service name:</b> <em>Analytics</em></li>
        <li><b>Endpoint:</b> <a href="http://kuiper:9081">http://kuiper:9081</a></li>
      </ul>
    </li><br/>
    <li>Konfigurisati servis:
      <ol>
        <li>Configuration tab > Source config > mqtt > Dodati novi
          <ul>
            <li><b>Name:</b> <em>mosquitto</em></li>
            <li><b>Server list:</b> <em>tcp://mqtt:1883</em> <u>(ovde obavezno pritisnuti ENTER!!!)</u></li>
            <li><b>Skip Certification verification:</b> <em>true</em></li>
          </ul>
        </li><br/>
        <li>Source tab > Create stream
          <ul>
            <li><b>Stream Name:</b> <em>agricultureStream</em></li>
            <li><b>Whether the schema stream:</b> <em>checked</em></li>
            <li><b>Stream fields:</b>
              <ul>
                <li><em>nitrogen - bigint</em></li>
                <li><em>phosphorus - bigint</em></li>
                <li><em>potassium - bigint</em></li>
                <li><em>temperature - float</em></li>
                <li><em>humidity - float</em></li>
                <li><em>ph - float</em></li>
                <li><em>rainfall - float</em></li>
                <li><em>cropType - string</em></li>
              </ul>
            </li>
            <li><b>Data Source:</b> <em>analytics/agriculture</em></li>
            <li><b>Stream Type:</b> <em>mqtt</em></li>
            <li><b>Configuration key:</b> <em>mosquitto</em></li>
            <li><b>Stream Format:</b> <em>json</em></li>
          </ul>
        </li><br/>
        <li>Rules > Create rule:
          <ul>
            <li><b>Rule ID:</b> <em>badSoilHealth</em></li>
            <li><b>SQL:</b> <em>SELECT * FROM aricultureStream WHERE humidity < 40 AND temperature > 32 OR ph < 4</em></li>
            <li><b>Actions:</b>
              <ul>
                <li><b>Sink:</b> <em>mqtt</em></li>
                <li><b>MQTT broker address:</b> <em>tcp://mqtt:1883</em></li>
                <li><b>MQTT topic:</b> <em>analytics/alerts</em></li>
                <li><b>Skip Certification verification:</b> <em>true</em></li>
              </ul>
            </li>
          </ul>
        </li>
      </ol>
      </li>
  </ol>
  <h2>Podesavanje influxdb-a</h2>
  Otvoriti <a href="http://localhost:8086">http://localhost:8086</a></br>
  <ul>Setup initial user
    <li><b>username:</b> <em>admin</em></li>
    <li><b>password:</b> <em>adminadmin</em></li>
    <li><b>initial organization name:</b> <em>org</em></li>
    <li><b>initial bucket name:</b> <em>bucket</em></li>
  </ul>
  Otvoriti <em>Load Data</em> > <em>Buckets</em>
  <ul>
    <li>Create Bucket</li>
     <li><b>Name:</b> <em>analytics-bucket</em></li>
     <li><b>Delete data:</b> <em>Never</em></li>
  </ul>
  Otvoriti <em>Load Data</em> > <em>API tokens</em>
  <ul>
    <li>Generate API Token - READ/WRITE option</li>
    <li><b>Description:</b> <em>analytics-token</em></li>
  </ul>
  Zatim kopirati novogenerisani token i izmeniti u fajlu <em>Projekat1/MicroService-Analytics/influx/influxWrapper.cs</em> u promenljivu <em> _token_dimitrije</em>.
  <h2>Pokretanje aplikacije</h2>
  Koraci za pokretanje drugog projekta:
  <ul>
    <li>Locirati se u folder ~/Projekat1</li>
    <li>Izvrsiti komandu <pre>docker compose -f "docker-compose.yml" up -d --build</pre></li>
  </ul>
  Otvoriti u browser-u adresu <a href="http://localhost:5170/swagger/index.html">http://localhost:5170/swagger</a></br>
  Otvoreni prozor prikazuje swagger ui koji olaksava testiranje funkcionalnosti api funkcija.</br>
  Simuliranje senzora izvrisiti pozivom funkcije <b>GenerateData</b> iz <b>Agriculture</b> controller-a sa test podacima: sleppSeconds: 0.5</br>
</body>
