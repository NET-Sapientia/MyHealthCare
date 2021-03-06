package com.example.myhealthcareapp.fragments.makeAppointment.medic

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.myhealthcareapp.R
import com.example.myhealthcareapp.adapters.BookAppointmentAdapter
import com.example.myhealthcareapp.model.response.Medic
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.wdullaer.materialdatetimepicker.date.DatePickerDialog;
import android.util.Log
import android.widget.Toast
import com.example.myhealthcareapp.MainActivity
import com.example.myhealthcareapp.cache.SharedPreferencesManager
import com.example.myhealthcareapp.data.v1.MyHealthCareViewModel
import com.example.myhealthcareapp.fragments.BaseFragment
import com.example.myhealthcareapp.fragments.myAppointments.MyAppointmentsFragment
import com.example.myhealthcareapp.util.OnItemClickListener
import com.example.myhealthcareapp.model.CustomDate
import com.example.myhealthcareapp.model.response.MakeAppointment
import com.example.myhealthcareapp.model.response.AvailableDate
import com.google.android.material.dialog.MaterialAlertDialogBuilder
import com.google.android.material.textfield.TextInputLayout
import kotlinx.android.synthetic.main.activity_main.*
import org.koin.android.ext.android.inject
import org.koin.androidx.viewmodel.ext.android.sharedViewModel
import org.koin.androidx.viewmodel.ext.android.viewModel
import java.text.SimpleDateFormat
import java.util.*

class BookAppointmentFragment : BaseFragment(),
    DatePickerDialog.OnDateSetListener,
    OnItemClickListener
{
    private lateinit var recyclerview: RecyclerView
    private lateinit var adapter: BookAppointmentAdapter
    private lateinit var dateButton: FloatingActionButton
    private lateinit var timeButton: FloatingActionButton
    private lateinit var bookAppointment: Button
    private lateinit var appointmentDate: TextView
    private lateinit var appointmentDateLayout: TextInputLayout
    private lateinit var appointmentTime: TextView
    private lateinit var appointmentTimeLayout: TextInputLayout
    private lateinit var calendar: Calendar
    private lateinit var formatter: SimpleDateFormat

    private lateinit var currentHospitalName: String
    private lateinit var currentHospitalId: String
    private lateinit var currentDepartmentName: String
    private lateinit var currentDepartmentId: String
    private lateinit var currentMedic: Medic

    private lateinit var selectedHospitalTextView: TextView
    private lateinit var selectedDepartmentTextView: TextView

    private val viewModel by viewModel<BookAppointmentViewModel>()
    private val sharedPreferencesManager: SharedPreferencesManager by inject()
    private lateinit var medics: MutableList<Medic>

    private val freeDays: MutableList<CustomDate> = mutableListOf()
    private val freeHours: MutableList<String> = mutableListOf()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_book_appointment, container, false)

        initFreeHours()
        currentHospitalName = arguments?.getString("hospitalName").toString()
        currentHospitalId = arguments?.getString("hospitalId").toString()
        currentDepartmentName = arguments?.getString("departmentName").toString()
        currentDepartmentId = arguments?.getString("departmentId").toString()

        calendar = Calendar.getInstance()
        formatter = SimpleDateFormat("yyyy/MM/dd", Locale.getDefault())
        recyclerview = view.findViewById(R.id.medic_recyclerview)
        selectedHospitalTextView = view.findViewById(R.id.selected_hospital)
        selectedDepartmentTextView = view.findViewById(R.id.selected_department)
        dateButton = view.findViewById(R.id.appointment_date_btn)
        timeButton = view.findViewById(R.id.appointment_time_btn)
        bookAppointment = view.findViewById(R.id.make_appointment_btn)
        appointmentDate = view.findViewById(R.id.appointment_date)
        appointmentDateLayout = view.findViewById(R.id.appointment_date_layout)
        appointmentTime = view.findViewById(R.id.appointment_time)
        appointmentTimeLayout = view.findViewById(R.id.appointment_time_layout)

        viewModel.uiState.observe(viewLifecycleOwner) { state ->
            when (state) {
                is BookAppointmentViewModel.UiState.WithMedics -> {
                    medics = state.medics.toMutableList()
                    initUI()
                }
                is BookAppointmentViewModel.UiState.AppointmentMade -> {
                    Toast.makeText(requireContext(), "Appointment added", Toast.LENGTH_SHORT).show()
                    (mActivity as MainActivity).replaceFragment(MyAppointmentsFragment(), R.id.fragment_container)
                    (mActivity as MainActivity).bottomNavigation.selectedItemId = R.id.my_appointments
                }
                is BookAppointmentViewModel.UiState.Error -> {
                    Toast.makeText(requireActivity(), "Something went wrong", Toast.LENGTH_LONG).show()
                }
                else -> Unit
            }
        }

        viewModel.getMedics(id = currentDepartmentId.toInt())

        return view
    }


    private fun initFreeHours() {
        var startHour = 9
        repeat(times = 3, action = { hour ->
            freeHours.add("${startHour + hour}:30 - ${startHour + hour + 1}:30")
            startHour += 2
        })
    }

    private fun initUI() {
        (mActivity as MainActivity).searchIcon.isVisible = false
        (mActivity as MainActivity).profileIcon.isVisible = true

        selectedHospitalTextView.text = currentHospitalName
        selectedDepartmentTextView.text = currentDepartmentName

        currentMedic = medics[0]
        adapter = BookAppointmentAdapter(medics, this)
        recyclerview.adapter = adapter
        recyclerview.setHasFixedSize(true)

        dateButton.setOnClickListener {
            val year = calendar.get(Calendar.YEAR)
            val month = calendar.get(Calendar.MONTH)
            val day = calendar.get(Calendar.DAY_OF_MONTH)

            val datePickerDialog = DatePickerDialog.newInstance(this, year, month, day)
            datePickerDialog.isThemeDark = true
            datePickerDialog.showYearPickerFirst(false)
            datePickerDialog.setTitle("Set appointment date")

            // Setting Min Date to today date
            val minDate = Calendar.getInstance()
            datePickerDialog.minDate = minDate

            // Setting Max Date to next 1 years
            val maxDate = Calendar.getInstance()
            maxDate[Calendar.YEAR] = year + 1
            datePickerDialog.maxDate = maxDate

            //Disable all SUNDAYS, SATURDAYS
            var loopDate: Calendar = minDate
            while (minDate.before(maxDate)) {
                val dayOfWeek = loopDate[Calendar.DAY_OF_WEEK]
                if (dayOfWeek == Calendar.SUNDAY || dayOfWeek == Calendar.SATURDAY) {
                    val disabledDays = arrayOfNulls<Calendar>(1)
                    disabledDays[0] = loopDate
                    datePickerDialog.disabledDays = disabledDays
                }
                minDate.add(Calendar.DATE, 1)
                loopDate = minDate
            }

            datePickerDialog.show(childFragmentManager, "DatePickerDialog")
        }
        timeButton.setOnClickListener {
            var checkedItem = 1

            MaterialAlertDialogBuilder(requireContext())
                .setTitle(resources.getString(R.string.select_time_interval))
                .setNeutralButton(resources.getString(R.string.cancel)) { _, _ -> }
                .setPositiveButton(resources.getString(R.string.ok)) { _, _ ->
                    if(freeHours.isNotEmpty()) {
                        appointmentTime.text = freeHours[checkedItem]
                    }
                }
                .setSingleChoiceItems(freeHours.toTypedArray(), checkedItem) { _, which ->
                    checkedItem = which
                }
                .show()
        }
        bookAppointment.setOnClickListener {
            if(validateInput()){
                val appointmentDates = appointmentTime.text.split(" - ")
                val startDate = appointmentDate.text.toString() + ", " + appointmentDates[0]
                val endDate = appointmentDate.text.toString() + ", " + appointmentDates[1]

                val summary = arrayOf(
                    "Hospital: $currentHospitalName",
                    "Department: $currentDepartmentName",
                    "Medic: " + currentMedic.name,
                    "Date & Time: " + appointmentDate.text + ", " + appointmentDates[0] + " - " + appointmentDates[1],
                )

                val appointment = MakeAppointment(
                    clientId = sharedPreferencesManager.getUserId(),
                    departmentId = currentDepartmentId.toInt(),
                    medicId = currentMedic.id,
                    startDate = "2022-04-20T08:45:37.102Z",
                    endDate = "2022-04-20T18:28:49.636Z",
                    notes = "Note is empty"
                )

                MaterialAlertDialogBuilder(requireContext())
                    .setTitle(resources.getString(R.string.summary))
                    .setItems(summary) {_, _ ->}
                    .setNeutralButton(resources.getString(R.string.cancel)) { _, _ -> }
                    .setPositiveButton(resources.getString(R.string.accept)) { _, _ ->
                        viewModel.makeAppointment(appointment)
                    }
                    .show()
            }
        }
    }

    override fun onDateSet(picker: DatePickerDialog?, year: Int, monthOfYear: Int, dayOfMonth: Int) {
        Log.d("DATE", "$year $monthOfYear $dayOfMonth")
        var month = (monthOfYear + 1).toString()
        if(monthOfYear + 1 < 10){
            month = "0${monthOfYear}"
        }
        var day = dayOfMonth.toString()
        if(dayOfMonth < 10){
            day = "0$dayOfMonth"
        }
        val date = "$year/$month/$day"
        appointmentDate.text = date
        appointmentTime.text = null
    }

    override fun onItemClick(position: Int) {
        currentMedic = medics[position]
        Log.d("Poz", currentMedic.toString())
        adapter.selectMedic(position)
        appointmentDate.text = null
        appointmentTime.text = null
    }

    private fun validateInput(): Boolean {
        appointmentDateLayout.error = null
        appointmentTimeLayout.error = null

        when{
            appointmentDate.text.toString().isEmpty() -> {
                appointmentDateLayout.error = getString(R.string.error)
                return false
            }
            appointmentTime.text.toString().isEmpty() -> {
                appointmentTimeLayout.error = getString(R.string.error)
                return false
            }
        }
        return true
    }
}