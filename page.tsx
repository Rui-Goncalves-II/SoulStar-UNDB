"use client";
import { useEffect, useState } from "react";
import { supabase } from "../app/lib/supabaseClient";

type Task = {
  id: number;
  title: string;
};

export default function Home() {
  const [task, setTask] = useState("");
  const [tasks, setTasks] = useState<Task[]>([]);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [user, setUser] = useState<any>(null);

  useEffect(() => {
    supabase.auth.getSession().then(({ data }) => {
      setUser(data.session?.user ?? null);
    });

    const { data: listener } = supabase.auth.onAuthStateChange((_event, session) => {
      setUser(session?.user ?? null);
    });

    return () => listener.subscription.unsubscribe();
  }, []);

  useEffect(() => {
    if (user) loadTasks();
  }, [user]);

  async function signIn() {
    const { error } = await supabase.auth.signInWithPassword({ email, password });
    if (error) alert("Erro ao entrar: " + error.message);
  }

  async function signUp() {
    const { error } = await supabase.auth.signUp({ email, password });
    if (error) alert("Erro ao cadastrar: " + error.message);
    else alert("Conta criada! Verifique seu e-mail.");
  }

  async function signOut() {
    await supabase.auth.signOut();
    setTasks([]);
  }

  // Salvar nova tarefa
  async function addTask() {
    await supabase.from("tasks").insert({ title: task });
    setTask("");
    loadTasks();
  }
  // Buscar todas as tarefas
  async function loadTasks() {
    const { data } = await supabase.from("tasks").select("*");
    setTasks(data ?? []);
  }
  // Carregar ao abrir a página
  useEffect(() => {
    loadTasks();
  }, []);

  // Deletar uma tarefa
  async function deletarTarefa(id:number) {
    await supabase.from("tasks").delete().eq("id", id);
    loadTasks();
  }

  if (!user) {
    return (
      <div style={{ padding: 40 }}>
        <h1>Login</h1>
        <input
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="E-mail"
          type="email"
        />
        <br /><br />
        <input
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="Senha"
          type="password"
        />
        <br /><br />
        <button onClick={signIn}>Entrar</button>
        <button onClick={signUp} style={{ marginLeft: 8 }}>Cadastrar</button>
      </div>
    );
  }

  return (
    <div style={{ padding: 40 }}>
      <h1>Lista de Tarefas</h1>
      <p>Logado como: {user.email}</p>
      <button onClick={signOut} style={{ marginBottom: 16, color: "red" }}>Sair</button>
      <br />
      <input
        value={task}
        onChange={(e) => setTask(e.target.value)}
        placeholder="Digite uma tarefa"
      />
      <button onClick={addTask}>Adicionar</button>
      <ul>
        {tasks.map((t) => (
          <li key={t.id}>
            {t.title}
            <button
              onClick={() => deletarTarefa(t.id)}
              style={{ marginLeft: 8, color: "red" }}>
              Excluir
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
