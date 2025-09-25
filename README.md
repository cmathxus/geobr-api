# GeoBR API

API REST para consulta de cidades do Brasil utilizando dados do IBGE. Permite buscar todas as cidades, filtrar por estado, nome ou ID do município.

---

## Tecnologias

* **.NET 6+ / C#**
* **ASP.NET Core Web API**
* **System.Text.Json**
* **Swagger / Swashbuckle**
* **HTTP Client** para consumir dados do IBGE

---

## Endpoints

### Listar todas as cidades

```
GET /cities
```

**Resposta 200**

```json
[
  {
    "id": 1100015,
    "municipio": "Alta Floresta D'Oeste",
    "estado": "RO",
    "regiao": "Norte"
  },
  ...
]
```

---

### Buscar cidade por ID

```
GET /cities/{id}
```

**Parâmetros:**

* `id` (int) → ID do cidade no IBGE


Exemplo:

```
GET /cities/1100015
```

---

### Buscar cidade por nome

```
GET /cities?name={nome}
```

### Filtrar cidades por estado

```
GET /cities?state={sigla_estado}
```

### Filtrar cidades por região

```
GET /cities?region={nome_regiao}
```
