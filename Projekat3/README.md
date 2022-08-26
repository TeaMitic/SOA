<body>
  <h1>Projekat 3</h1>
  Aplikacija koja pribavlja podatke sa senzora o temperaturi i vlaznosti zemljista i vrsi analizu prikupljenih podataka. U slucaju manje vlaznosti zemjista se salje naredba pumpi za vodu da se ugasi, u slucaju povecane vlaznosti salje se naredba za gasenje pumpe ukoliko je upaljena.
  Aplikacija radi u docker-u.
  <ul>Mikroservisi koji se koriste u ovom projektu su:
    <li>MicroService-Monitoring</li>
    <li>MicroService-Visualization</li>
    <li>testApp</li>
  </ul>
  <h2>Podesavanje influxdb-a</h2>
  Otvoriti <a href="http://localhost:8086">http://localhost:8086</a></br>
  <ul>Setup initial user
    <li><b>username:</b> <em>admin</em></li>
    <li><b>password:</b> <em>adminadmin</em></li>
    <li><b>initial organization name:</b> <em>organization</em></li>
    <li><b>initial bucket name:</b> <em>bucket</em></li>
  </ul>
  Otvoriti <em>Load Data</em> > <em>Buckets</em>
  <ul>
    <li>Create Bucket</li>
     <li><b>Name:</b> <em>visualization-bucket</em></li>
     <li><b>Delete data:</b> <em>Never</em></li>
  </ul>
  Otvoriti <em>Load Data</em> > <em>API tokens</em>
  <ul>
    <li>Generate API Token - READ/WRITE option</li>
    <li><b>Description:</b> <em>visualization-token</em></li>
  </ul>
  Zatim kopirati novogenerisani token i izmeniti u fajlu <em>Projekat3/MicroService-Visualization/index.js</em> u promenljivu <em>token</em>.
  <h2>Setovanje Grafane</h2> 
  Podešavanje vizuelizacije generisanih podataka u grafani
  <ul>
    <li>U pretraživaču uneti <a href="http://localhost:4200">http://localhost:4200</a></li>
    <li>Ulogovati se
      <ul>
        <li>admin</li>
        <li>admin</li>
        <li>Skip</li>
      </ul>
    </li>
    <li>Configuration > Data Sources > Add new data source > InfluxDB</li>
    <li><b>Query Language:</b> <em>Flux</em> (zbog verzije 2.x influxdb-ja)
    <li>Uneti
      <ul>
        <li><b>URL:</b> 		<em>http://influx:8086</em></li>
        <li><b>Organization:</b> 	<em>ogranization</em></li>
        <li><b>Token:</b> 		<em>GENERISANI TOKEN</em></li>
        <li><b>Default Bucket:</b> 	<em>visualization-bucket</em></li>
        <li>Sve ostalo ostaje isto</li>
    </ul>
    </li>
    <li>Save & Test</li>
    <li>Dashboards > New dashboard > Add a new panel</li>
    <li>Odabrati
      <ul>
          <li><b>Flux Query:</b><pre>from(bucket: "visualization-bucket")</br>    |> range(start: -20m)</br>    |> filter(fn: (r) => r._meausurement == "visualization-bucket" and r._field == "temperature")</br>    |> window(every: 1m)</pre></li>
      </ul>
    </li>
    <li>Apply</li>
  </ul>
  <h2>Pokretanje aplikacije</h2>
   Koraci za pokretanje treceg projekta:
   <ul>
    <li>Locirati se u folder ~/Projekat3</li>
    <li>Izvrsiti komandu <pre>docker compose -f "docker-compose.yml" up -d --build</pre></li>
  </ul>
  Locirati se u folderu <i>Projekat3/loader</i></br>
  Izvrsiti komandu <pre>npm install</pre> a zatim <pre>npm start</pre></br>
  Nadgledanje akcija se moze ispratiti u konzoli loader aplikacije, u konzoli testApp servisa kao i u grafani.
  
</body>
