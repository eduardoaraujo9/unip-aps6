import javax.swing.JOptionPane;

public class Biometria {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		JOptionPane.showMessageDialog(null, "Software de Biometria");
		
		CGMat vetorOriginal = new CGMat();
		CGMat vetorTeste = new CGMat();
		
		// ao final validar com subtração = 0
		if (vetorTeste.Subtracao(vetorOriginal) < 15) {
			JOptionPane.showMessageDialog(null, "Bem vindo ao banco de dados do meio ambiente!");
		}
	}

}
