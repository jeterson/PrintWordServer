# Servidor HTTP para impressão de documentos word.
 Baixe o arquivo WindowsExe.zip. Descompacte e execute o arquivo `PrintWordServerApp.exe`
 <br/>
 Inicie o servidor.
 Utilize seu programa de requisicão HTTP favorito. Por exemplo o Postman.
 
 
 #### Listando Impressoras
 `http://localhost:5170/ListPrinters`
 <br /> 
 Response:
 ```
 [
    {
        "Name": "Microsoft Print to PDF",
        "Default": true
    },
 ]
 ```
 
  
#### Imprimindo um documento
Crie um arquivo em `C:\temp\Teste.docx`
<br />
Envie uma requisição com o seguinte corpo:
```
{
	"inputFileName": "Teste.docx",
	"basePath": "C:\\temp",
	"basePathReplaced": "replaced"	
}
```
O documento será impresso na impressora padrão.

# Substituindo Palavras
Recurso: `http://localhost:5170/Print` <br/>
Body Request:
```
{
	"inputFileName": "Teste.docx",
	"basePath": "C:\\temp",
	"basePathReplaced": "replaced",
	"keyPrefix": "&",
	"replaces": [
		{
			"key": "nome",
			"value": "Jeterson"
		}
	]
}
```
`KeyPrefix`: Carectere identificador da palavra a ser substituida<br />
`replaces.key`: Chave a ser substituida<br />
`replaces.value`: Valor a ser subtituido<br />

Crie um Doc com o conteudo por exemplo: `Olá &nome` e envie uma requisição para o servidor.
<br/>
Será impresso `Olá Jeterson`
