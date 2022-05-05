package com.example.myhealthcareapp.adapters

import android.annotation.SuppressLint
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.myhealthcareapp.R
import com.example.myhealthcareapp.model.response.ClientAppointment
import com.example.myhealthcareapp.model.response.ClientAppointmentResponse
import com.google.android.material.dialog.MaterialAlertDialogBuilder
import kotlinx.android.synthetic.main.medic_appointment_element.view.*

class MedicAppointmentAdapter(private var appointmentList : MutableList<ClientAppointment>) : RecyclerView.Adapter<MedicAppointmentAdapter.MedicAppointmentViewHolder>() {

    inner class MedicAppointmentViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView){
        val patientName : TextView = itemView.patient_name
        val medicalDepartmentName : TextView = itemView.department_name
        val appointmentDate : TextView = itemView.appointment_start_date
        private val detailsButton : Button = itemView.details_button

        init {
            detailsButton.setOnClickListener {
                val summary = arrayOf(
                    "Patient: ${patientName.text}",
                    "Department: ${medicalDepartmentName.text}",
                    "Medic: " + "Asd",
                    "Date & Time: ${appointmentDate.text}",
                )

                MaterialAlertDialogBuilder(itemView.context)
                    .setTitle("Appointment details")
                    .setItems(summary) {_, _ ->}
                    .show()
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MedicAppointmentViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.medic_appointment_element, parent, false)
        return MedicAppointmentViewHolder(view)
    }

    @SuppressLint("SetTextI18n")
    override fun onBindViewHolder(holder: MedicAppointmentViewHolder, position: Int) {
        val itemsViewModel = appointmentList[position]
        holder.patientName.text = itemsViewModel.medicName
        holder.medicalDepartmentName.text = itemsViewModel.departmentName
        holder.appointmentDate.text = itemsViewModel.startDate + " - " + itemsViewModel.endDates
    }

    override fun getItemCount(): Int = appointmentList.size
}