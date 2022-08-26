<body>
  <h1>Projekat 2</h1>
  Aplikacija koja pribavlja podatke sa senzora o temperaturi i vlaznosti zemljista i vrsi analizu prikupljenih podataka. U slucaju manje vlaznosti zemjista se salje naredba pumpi za vodu da se ugasi, u slucaju povecane vlaznosti salje se naredba za gasenje pumpe ukoliko je upaljena.
  Aplikacija radi u docker-u.
  <ul>Mikroservisi koji se koriste u ovom projektu su:
    <li>MicroService-Monitoring</li>
    <li>MicroService-Visualization</li>
    <li>testApp</li>
  </ul>
  <h2>Podesavanje influxdb-a</h2>
    <ul>
      <li>Organization: <i>org</i></li>
      <li>Bucket: <i>bucket</i></li>
      <li>Username: <i>admin</i></li>
      <li>Password: <i>adminadmin</i></li>
      <li>Generisati token za RW operacije</li>
      <li>Izmeniti u kodu generisani token za promenljivu <i>token</i> u fajlu <b>index.js</b>. Putanja: <i>Projekat3/MicroService-Visualization/index.js</i></li>
  <h2>Pokretanje aplikacije</h2>
   <ul>Koraci za pokretanje drugog projekta:
    <li>Locirati se u folder ~/Projekat3</li>
    <li>Izvrsiti komandu <pre>docker compose -f "docker-compose.yml" up -d --build</pre></li>
  </ul>
  Locirati se u folderu <i>Projekat3/loader</i></br>
  Izvrsiti komandu <pre>npm install</pre> a zatim <pre>npm start<pre>.</br>
  Nadgledanje akcija se moze ispratiti u konzoli loader aplikacije, u konzoli testApp servisa kao i u grafani. 
</body>
