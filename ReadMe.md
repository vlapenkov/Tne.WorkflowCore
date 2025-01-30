Создан для того чтобы спроектировать одновременно  2 запущенных workflow
в одной базе и одной схеме:

dotnet run ENV_WORKFLOW=ApprovalWorkflow --urls=http://localhost:5001/

dotnet run ENV_WORKFLOW=SecondWorkflow --urls=http://localhost:5002/