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
  
</body>
